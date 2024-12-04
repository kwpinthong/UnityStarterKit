using UnityEditor;
using UnityEngine;

namespace StarterKit.Common.Editor
{
    public static class AssetCreator
    {
        private static string prefabPath = "Assets/Plugins/StarterKit/Prefabs";
        private static string createPath = "Assets/Resources/StarterKit/Prefabs";

        [MenuItem("StartKit/Create/Function String")]
        public static void CreateFunctionStringPrefabVar() => CreatePrefab("FunctionString");

        [MenuItem("StartKit/Create/Audio Manager")]
        public static void CreateAudioManagerPrefabVar() => CreatePrefab("AudioManager");

        [MenuItem("StartKit/Create/Pool Manager")]
        public static void CreatePoolManagerPrefabVar() => CreatePrefab("PoolManager");

        private static void CreatePrefab(string prefabName)
        {
            var prefab = AssetFinder.Find<GameObject>(prefabName, prefabPath);
            if (prefab == null)
            {
                Debug.LogError($"Prefab {prefabName} not found");
                return;
            }
            CreatePathIfNotExist();
            var prefabInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject; 
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, $"{createPath}/{prefabName}.prefab");
            Object.DestroyImmediate(prefabInstance);
        }

        private static void CreatePathIfNotExist()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
        
            if (!AssetDatabase.IsValidFolder("Assets/Resources/StarterKit"))
                AssetDatabase.CreateFolder("Assets/Resources", "StarterKit");
        
            if (!AssetDatabase.IsValidFolder("Assets/Resources/StarterKit/Prefabs"))
                AssetDatabase.CreateFolder("Assets/Resources/StarterKit", "Prefabs");
        }
    }
}