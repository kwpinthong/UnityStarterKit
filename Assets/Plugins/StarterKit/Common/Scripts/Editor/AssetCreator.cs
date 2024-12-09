using UnityEditor;
using UnityEngine;

namespace StarterKit.Common.Editor
{
    public static class AssetCreator
    {
        private const string resourceFolder = "Resources";
        private const string starterKitFolder = "StarterKit";
        private const string prefabsFolder = "Prefabs";
        
#if STARTERKIT_PACKAGE_MANAGER
        // version import with package manager
        private static string prefabPath = "Packages/unitystarterkit/Prefabs";
#else
        // version import with unitypackage
        private static string prefabPath = "Assets/Plugins/StarterKit/Prefabs";
#endif
        
        private static string createPath = "Assets/Resources/StarterKit/Prefabs";
        
        private static string assetResourcePath => "Assets/Resources";
        private static string assetResourceStarterKitPath => $"{assetResourcePath}/StarterKit";
        private static string assetResourceStarterKitPrefabsPath => $"{assetResourceStarterKitPath}/Prefabs";
        

        [MenuItem("StartKit/Create/Function String")]
        public static void CreateFunctionStringPrefabVar() => CreatePrefab("FunctionString");

        [MenuItem("StartKit/Create/Audio Manager")]
        public static void CreateAudioManagerPrefabVar() => CreatePrefab("AudioManager");

        [MenuItem("StartKit/Create/Pool Manager")]
        public static void CreatePoolManagerPrefabVar() => CreatePrefab("PoolManager");
        
        [MenuItem("StartKit/Create/Localize")]
        public static void CreateLocalizePrefabVar() => CreatePrefab("Localize");

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
            CommonDebug.Log($"Prefab, {prefabName}, created at, {createPath}");
        }
        
        private static void CreatePathIfNotExist()
        {
            var isResourceExist = AssetDatabase.IsValidFolder(assetResourcePath);
            var isStarterKitExist = AssetDatabase.IsValidFolder(assetResourceStarterKitPath);
            var isPrefabsExist = AssetDatabase.IsValidFolder(assetResourceStarterKitPrefabsPath);
            
            if (!isResourceExist)
                CreateFolder("Assets", resourceFolder);
            
            if (!isStarterKitExist)
                CreateFolder(assetResourcePath, starterKitFolder);
            
            if (!isPrefabsExist)
                CreateFolder(assetResourceStarterKitPath, prefabsFolder);
        }
        
        private static void CreateFolder(string path, string folderName)
        { 
            AssetDatabase.CreateFolder(path, folderName);
        }
    }
}