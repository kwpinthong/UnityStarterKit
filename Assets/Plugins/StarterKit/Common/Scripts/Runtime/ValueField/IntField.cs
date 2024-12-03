using UnityEngine;

namespace StarterKit.Common.ValueField
{
    [CreateAssetMenu(fileName = "IntField", menuName = "StarterKit/ValueField/IntField")]
    public class IntField : ScriptableObject
    {
        [SerializeField] private int value;
        
        public int Value => value;

        public void Set(int v) => value = v;
        
        public void Add(int addVar) => value += addVar;
        
        public void Remove(int removeVar) => value -= removeVar;
    }
}
