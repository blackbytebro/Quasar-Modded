using System.Management;
using System.Diagnostics;

namespace Quasar.Client.Helper.Network
{
    public static class CommandHelper
    {
        public static void Execute(ProcessStartInfo info)
        {
            ConnectionOptions options = new ConnectionOptions
            {
                Username = "remoteUsername",
                Password = "remotePassword"
            };

            ManagementScope scope = new ManagementScope(@"\\RemoteMachineName\root\cimv2", options);
            scope.Connect();

            ManagementClass processClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
            ManagementBaseObject inParams = processClass.GetMethodParameters("Create");
            inParams["CommandLine"] = "";

            ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);
        }
    }
}
