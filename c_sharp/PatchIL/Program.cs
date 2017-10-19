using Microsoft.Build.Utilities;
using System.Diagnostics;
using System.IO;

namespace PatchIL
{
    class Program
    {
        static void Main(string[] args)
        {
            string assemblyPath = args[0];
            FileInfo assemblyFile = new FileInfo(assemblyPath);
            string assemblyFileName = assemblyFile.Name;
            Directory.SetCurrentDirectory(assemblyFile.Directory.FullName);
            string ilName = "ilcode.il";
            TargetDotNetFrameworkVersion sdkVersion = TargetDotNetFrameworkVersion.VersionLatest;
            string ildasmPath = ToolLocationHelper.GetPathToDotNetFrameworkSdkFile("ildasm.exe", sdkVersion);

            /* Где-то это работает, у меня нет */
            string ilasmPath = ToolLocationHelper.GetPathToDotNetFrameworkSdkFile("ilasm.exe", sdkVersion);
            /* В какой-то библиотеке используется такой "хак" */
            if (string.IsNullOrEmpty(ilasmPath))
                ilasmPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ilasm.exe";

            string ildasmArgs = string.Format("/out={0} {1}", ilName, assemblyFileName);
            string ilasmArgs = string.Format("/out={0} {1}", assemblyFileName, ilName);

            ProcessStartInfo ildasmStart = new ProcessStartInfo(ildasmPath, ildasmArgs);
            ildasmStart.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessStartInfo ilasmStart = new ProcessStartInfo(ilasmPath, ilasmArgs);
            ilasmStart.WindowStyle = ProcessWindowStyle.Hidden;

            Process p;
            
            p = Process.Start(ildasmStart);
            p.WaitForExit();

            string ilCode = File.ReadAllText(ilName);
            string ilModified = ilCode.Replace("Hello, World!", "Goodbye, World!");

            File.WriteAllText(ilName, ilModified);

            p = Process.Start(ilasmStart);
            p.WaitForExit();
        }
    }
}
