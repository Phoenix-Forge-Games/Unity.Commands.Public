using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

namespace PhoenixForgeGames.Commands.Public.EditorScripts
{

    [InitializeOnLoad]
    public static class AutoCreateUserContent
    {
        public static bool HasRun => File.Exists(MARKER_PATH);
        
        private const string MARKER_PATH = "Library/PhoenixForge_Commands_Setup.txt";
        private const string PACKAGE_NAME = "com.phoenixforgegames.commands.public";

        static AutoCreateUserContent()
        {
            if (!HasRun)
            {
                RunOnce();
            }
        }

        public static void RunOnce()
        {
            if (File.Exists(MARKER_PATH)) return;
            
            Debug.Log("[PhoenixForge] Running user content setup...");

            var packageRoot = $"Packages/{PACKAGE_NAME}/Runtime";
            var scenesSource = $"{packageRoot}/DemoScenes";
            var objectsSource = $"{packageRoot}/Objects";

            var scenesTarget = "Assets/PhoenixForgeGames/Commands/Scenes";
            var objectsTarget = "Assets/PhoenixForgeGames/Commands/Objects";

            CreateFolder(scenesTarget);
            CreateFolder(objectsTarget);

            CopyAllAssetsInFolder(scenesSource, scenesTarget);
            CopyAllAssetsInFolder(objectsSource, objectsTarget);

            File.WriteAllText(MARKER_PATH, "Folders and demo content created.");
            AssetDatabase.Refresh();
        }

        private static void CreateFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            Directory.CreateDirectory(path);
            Debug.Log($"[PhoenixForge] Created folder: {path}");
        }

        private static void CopyAllAssetsInFolder(string sourceDir, string destDir)
        {
            if (!Directory.Exists(sourceDir))
            {
                Debug.LogWarning($"[PhoenixForge] Source directory missing: {sourceDir}");
                return;
            }

            var assetFiles = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".asset") || f.EndsWith(".prefab") || f.EndsWith(".unity"));

            foreach (var file in assetFiles)
            {
                var relativePath = file.Substring(sourceDir.Length + 1); // Preserve subfolder structure
                var targetPath = Path.Combine(destDir, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
                File.Copy(file, targetPath, overwrite: true);
                CopyMetaFile(file, targetPath);
                Debug.Log($"[PhoenixForge] Copied asset: {file} → {targetPath}");
            }
        }

        private static void CopyMetaFile(string sourcePath, string destPath)
        {
            var metaSource = sourcePath + ".meta";
            var metaDest = destPath + ".meta";
            if (File.Exists(metaSource))
            {
                File.Copy(metaSource, metaDest, overwrite: true);
            }
        }
    }
}