using UnityEngine;

namespace StarterKit.Common
{
    public static class CreateInstance
    {
        private const string prefabPath = "StarterKit/Prefabs";
        
        public static GameObject Create(string prefabName)
        {
            var prefab = Resources.Load<GameObject>($"{prefabPath}/{prefabName}");
            var createdInstance = Object.Instantiate(prefab);
            createdInstance.name = prefabName;
            return createdInstance;
        }
    }
}

