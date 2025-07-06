using UnityEditor;

namespace PhoenixForgeGames.Commands.Public.EditorScripts
{
    public class AutoCreateUserContentPostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(
            string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (!AutoCreateUserContent.HasRun)
            {
                AutoCreateUserContent.RunOnce();
            }
        }
    }
}