#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace StarterKit.Common.Editor
{
    public static class AssetFinder
    {
        public static T Find<T>(string fileName, string path = "") where T : Object
        {
#if UNITY_EDITOR
            string filter = $"t:{typeof(T).Name} {fileName}";
            string[] searchInFolders = string.IsNullOrEmpty(path) ? null : new[] { path };
            
            var guids = AssetDatabase.FindAssets(filter, searchInFolders);
            if (guids.Length == 0)
                return null;
            
            path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var result = AssetDatabase.LoadAssetAtPath<T>(path);
            if (result != null)
            {
                var resultName = result.name.ToLower();
                resultName = resultName.Replace(" ", "");
                var fileNameLower = fileName.ToLower();
                fileNameLower = fileNameLower.Replace(" ", "");
                if (resultName == fileNameLower)
                    return result;
            }
#endif
            return null;
        }
    }
}