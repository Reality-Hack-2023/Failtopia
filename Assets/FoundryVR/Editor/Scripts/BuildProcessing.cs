using System;
using System.IO;
using Microsoft.Win32;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Foundry
{
    public class BuildProcessing : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            IncrementBuildVersion();
        }
        
        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.result == BuildResult.Failed) return;
            
            ExportVersionFile(report.summary.outputPath);

            if (report.summary.platformGroup != BuildTargetGroup.Standalone) return;
            if (!EditorUtility.DisplayDialog(
                    "Create Installer", 
                    "Do you want to create an installer?" 
                        + Environment.NewLine
                        + Environment.NewLine
                        + "This will create an installer for the current build that can be used to distribute to other users.", 
                    "Yes", 
                    "No"))
                return;
            
            RunInstallerCompiler();
        }

        void IncrementBuildVersion()
        {
            Version version;
            if (!Version.TryParse(PlayerSettings.bundleVersion, out version)) return;
            
            version = new Version(version.Major, version.Minor, version.Build + 1);
            PlayerSettings.bundleVersion = version.ToString();
        }
        
        void ExportVersionFile(string outputPath)
        {
            string buildPath = new FileInfo(outputPath).Directory.FullName;
            string versionPath = Path.Combine(buildPath, "Version.txt");
            File.WriteAllText(versionPath, PlayerSettings.bundleVersion);
        }

        void RunInstallerCompiler()
        {
            string innoCompilerPath;
            if (!TryGetInnoSetupCompilerPath(out innoCompilerPath)) return;
            
            string installationPath = Path.Combine(Application.dataPath, "../Installers/");
            string installerPath = Path.Combine(installationPath, "installer.iss");

            if (File.Exists(innoCompilerPath) && File.Exists(installerPath))
            {
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = innoCompilerPath;
                process.StartInfo.Arguments = $"/cc \"{installerPath}\"";
                process.Start();
            }
        }

        bool TryGetInnoSetupCompilerPath(out string path)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\InnoSetupScriptFile\shell\open\command");
            if (key != null)
            {
                var value = key.GetValue("") as string;
                foreach (var entry in value.Split('\"'))
                {
                    if (!entry.EndsWith(".exe")) continue;
                    path = entry;
                    return true;
                }
            }
            
            Debug.LogError("Inno Setup Compiler not found. Please go to https://jrsoftware.org/isdl.php and install it before running again.");
            path = null;
            return false;
        }
    }
}
