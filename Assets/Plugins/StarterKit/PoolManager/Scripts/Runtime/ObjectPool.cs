using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace StarterKit.PoolManagerLib
{
    [System.Serializable]
    public class ObjectPool : MonoBehaviour
    {
        public string Id;
        [SerializeField] private GameObject perfab;
        [SerializeField] private List<GameObject> pool;
        
        public void Setup(GameObject perfab)
        {
            this.perfab = perfab;
            Id = this.perfab.name;
            gameObject.name = Id;
            pool = new List<GameObject>();
        }

        public GameObject GetObject(bool active = true, Vector3 position = default, Quaternion rotation = default)
        {
            var newObj = pool.Find(obj => !obj.activeSelf);
            if (newObj == null)
            {
                newObj = Instantiate(perfab, transform);
                newObj.name = $"prefab_{perfab.name}_{pool.Count + 1}";
                pool.Add(newObj);
            }
            newObj.transform.position = position;
            newObj.transform.rotation = rotation;
            newObj.SetActive(active);
            return newObj;
        }

        public void ClearAll()
        {
            foreach (var i in pool)
                i.gameObject.SetActive(false);
        }
    }
}
