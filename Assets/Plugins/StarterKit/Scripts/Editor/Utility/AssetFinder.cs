using UnityEngine;

namespace StarterKit.Editor.Utility
{
    public static class AssetFinder
    {
        public static T Find<T>(string fileName, string path = "") where T : Object
        {
            string filter = $"t:{typeof(T).Name} {fileName}";
            string[] searchInFolders = string.IsNullOrEmpty(path) ? null : new[] { path };
            var guids = UnityEditor.AssetDatabase.FindAssets(filter, searchInFolders);
            if (guids.Length == 0)
            {
                return null;
            }
            path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
            var result = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            if (result != null)
            {
                var resultName = result.name.ToLower();
                resultName = resultName.Replace(" ", "");
                var fileNameLower = fileName.ToLower();
                fileNameLower = fileNameLower.Replace(" ", "");
                if (resultName == fileNameLower)
                {
                    return result;
                }
            }
            return null;
        }
    }
}