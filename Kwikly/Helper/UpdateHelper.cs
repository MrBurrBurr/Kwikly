using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace Kwikly {
    class UpdateHelper {
        private static string updateVBSFile = Path.GetTempPath() + "updateKwikly.vbs";
        private static string updateFile = Path.GetTempPath() + "Kwikly.exe";
        private static string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static string CreateUpdateVBS() {
            StreamWriter sw = new StreamWriter(updateVBSFile);
            sw.WriteLine("Const DestinationFile = " + "\"" + currentPath + @"\Kwikly.exe""");
            sw.WriteLine("Const DestinationPath = " + "\"" + currentPath + "\\\"");
            sw.WriteLine("Const SourceFile = " + "\"" + updateFile + "\"");
            sw.WriteLine("WScript.Sleep 1000");
            sw.WriteLine("Set fso = CreateObject(\"" + "Scripting.FileSystemObject\"" + ")");
            sw.WriteLine("If Not fso.GetFile(DestinationFile).Attributes And 1 Then");
            sw.WriteLine("  fso.CopyFile SourceFile, DestinationPath, True");
            sw.WriteLine("Else");
            sw.WriteLine("  fso.GetFile(DestinationFile).Attributes = fso.GetFile(DestinationFile).Attributes - 1");
            sw.WriteLine("  fso.CopyFile SourceFile, DestinationPath, True");
            sw.WriteLine("  fso.GetFile(DestinationFile).Attributes = fso.GetFile(DestinationFile).Attributes + 1");
            sw.WriteLine("End If");
            sw.WriteLine("Set fso = Nothing");
            sw.WriteLine("CreateObject(\"" + "WScript.Shell\"" + ").Run \"\"\"" + currentPath + @"\Kwikly.exe""""""");
            sw.Close();
            return updateVBSFile;
        }

        public static void CleanUpAfterUpdate() {
            if (File.Exists(updateVBSFile))
                File.Delete(updateVBSFile);

            if (File.Exists(updateFile))
                File.Delete(updateFile);
        }

        public static void DownloadFileToTempPath(ProgressBar pBar, string url) {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += (s, e) => {
                pBar.Value = e.ProgressPercentage;
            };
            client.DownloadFileCompleted += (s, e) => {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = CreateUpdateVBS();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process proc = Process.Start(startInfo);
                Application.Exit();
            };
            client.DownloadFileAsync(new Uri(url), Path.GetTempPath() + "Kwikly.exe");
            client.Dispose();
        }
    }
}
