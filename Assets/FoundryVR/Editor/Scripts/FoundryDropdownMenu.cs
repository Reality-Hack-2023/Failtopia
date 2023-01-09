using System.IO;
using UnityEditor;
using UnityEngine;

namespace Foundry
{
    public class FoundryDropdownMenu : EditorWindow
    {
        [MenuItem("Foundry/Quick Build")]
        public static void QuickBuildMenuItem()
        {
            string buildPath = Path.Combine(Application.dataPath, "../Builds");
            if (!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }
        
            string exePath = Path.Combine(buildPath, Application.productName + ".exe");
        
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, exePath, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
        }
    }
}
