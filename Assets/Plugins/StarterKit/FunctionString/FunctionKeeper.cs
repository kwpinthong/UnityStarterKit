using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterKit.FunctionStringLib
{
    /// <summary>
    /// The FunctionKeeper class is responsible for managing and running functions based on a key.
    /// </summary>
    public class FunctionKeeper : MonoBehaviour
    {
        /// <summary>
        /// A list of function information data.
        /// </summary>
        [SerializeField] private List<FunctionKeeperData> functionInfos;
        
        /// <summary>
        /// Runs the function associated with the specified key.
        /// </summary>
        /// <param name="functionKey">The key of the function to run.</param>
        public void Run(string functionKey)
        {
            foreach (var function in functionInfos)
            {
                if (function.Key == functionKey && function.CanRun()) 
                    FunctionString.Run(function.Function, function.InspectorValues, null);
            }
        }
        
        /// <summary>
        /// Runs the function associated with the specified key and executes a callback upon completion.
        /// </summary>
        /// <param name="functionKey">The key of the function to run.</param>
        /// <param name="onComplete">The callback to execute upon completion.</param>
        public void Run(string functionKey, System.Action onComplete)
        { 
            var index = functionInfos.FindIndex(f => f.Key == functionKey && f.CanRun());
            
            if (index == -1) 
                return;
            
            var function = functionInfos[index];
            FunctionString.Run(function.Function, function.InspectorValues, onComplete);
        }
    }
}