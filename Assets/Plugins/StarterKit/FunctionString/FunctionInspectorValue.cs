using UnityEngine;

namespace StarterKit.Common.FunctionStringLib
{
    /// <summary>
    /// Represents a key-value pair where the key is a string and the value is a Transform.
    /// </summary>
    [System.Serializable]
    public struct FunctionInspectorValue
    {
        /// <summary>
        /// The key associated with the value.
        /// </summary>
        public string Key;

        /// <summary>
        /// The Transform value associated with the key.
        /// </summary>
        public Transform Value;
    }
}