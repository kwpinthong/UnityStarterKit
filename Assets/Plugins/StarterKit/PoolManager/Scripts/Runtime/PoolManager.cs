using System.Collections;
using System.Collections.Generic;
using StarterKit.Common;
using UnityEngine;

namespace StarterKit.PoolManagerLib
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager instance;
        
        public static GameObject GetGameObject(GameObject prefab, bool active = true, Vector3 position = default, Quaternion rotation = default)
        {
            if (instance == null)
                CreateInstance.Create("PoolManager");
            return instance.ThisGetGameObject(prefab, active, position, rotation);
        }

        [SerializeField]
        private bool dontDestroyOnLoad = true;
        [SerializeField] 
        private ObjectPool poolPrefab;
        [SerializeField] 
        private List<ObjectPool> pools;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
            pools = new List<ObjectPool>();
        }
        
        private void OnDestroy()
        { 
            instance = null;
        }
        
        private GameObject ThisGetGameObject(GameObject prefab, bool active, Vector3 position, Quaternion rotation)
        {
            var pool = pools.Find(p => p.Id == prefab.name);
            if (pool == null)
            {
                pool = Instantiate(poolPrefab, transform);
                pool.Setup(prefab);
                pools.Add(pool);
            }
            return pool.GetObject(active, position, rotation);
        }
    }
}
