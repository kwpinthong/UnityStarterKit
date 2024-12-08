using System.Collections.Generic;
using UnityEngine;

namespace StarterKit.FunctionStringLib
{
    /// <summary>
    /// Represents the data required to keep track of a function.
    /// </summary>
    [System.Serializable]
    public struct FunctionKeeperData
    {
        /// <summary>
        /// The key associated with the function.
        /// </summary>
        public string Key;

        /// <summary>
        /// The function to be executed.
        /// </summary>
        public string Function;

        /// <summary>
        /// A list of inspector values associated with the function.
        /// </summary>
        public List<FunctionInspectorValue> InspectorValues;

        /// <summary>
        /// Determines whether the function can be run.
        /// </summary>
        /// <returns>True if the function is not null or empty; otherwise, false.</returns>
        public bool CanRun() => !string.IsNullOrEmpty(Function);
    }
}