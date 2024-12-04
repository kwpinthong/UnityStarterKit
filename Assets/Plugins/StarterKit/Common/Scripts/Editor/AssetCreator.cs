using UnityEditor;
using UnityEngine;

namespace StarterKit.Common.Editor
{
    public class AssetCreator
    {
        private static string prefabPath = "Assets/Plugins/StarterKit/Prefabs";
        private static string createPath = "Assets/Resources/StarterKit/Prefabs";
        
        [MenuItem("StartKit/Create/Function String")]
        public static void CreateFunctionStringPrefabVar()
        {
            CreatePrefab("FunctionString");
        }
        
        [MenuItem("StartKit/Create/Audio Manager")]
        public static void CreateAudioManagerPrefabVar()
        {
            CreatePrefab("AudioManager");
        }
        
        [MenuItem("StartKit/Create/Pool Manager")]
        public static void CreatePoolManagerPrefabVar()
        {
            CreatePrefab("PoolManager");
        }
        
        private static void CreatePrefab(string prefabName)
        {
            var prefab = AssetFinder.Find<GameObject>(prefabName, prefabPath);
            if (prefab == null)
            {
                Debug.LogError($"Prefab {prefabName} not found");
                return;
            }
            CreatePathIfNotExist();
            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject; 
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, $"{createPath}/{prefabName}.prefab");
            Object.DestroyImmediate(prefabInstance);
        }
        
        private static void CreatePathIfNotExist()
        {
            var isResourceExist = AssetDatabase.IsValidFolder("Assets/Resources");
            if (!isResourceExist)
                AssetDatabase.CreateFolder("Assets", "Resources");
                
            var isStarterKitExist = AssetDatabase.IsValidFolder("Assets/Resources/StarterKit");
            if (!isStarterKitExist)
                AssetDatabase.CreateFolder("Assets/Resources", "StarterKit");
                
            var isPrefabsExist = AssetDatabase.IsValidFolder("Assets/Resources/StarterKit/Prefabs");
            if (!isPrefabsExist)
                AssetDatabase.CreateFolder("Assets/Resources/StarterKit", "Prefabs");
        }
    }
}