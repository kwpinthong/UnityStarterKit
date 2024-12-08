using System;
using System.Collections;
using System.Collections.Generic;
using StarterKit.Common;
using UnityEngine;

namespace StarterKit.Localize.Runtime
{
    public class Localize : MonoBehaviour
    {
        private static Localize instance;

        public static List<string> CodeList = new List<string>
        {
            "EN",
            "TH"
        };

        public static string GetLocalizeText(string key, string localizeCode = "EN")
        {
            if (instance == null)
                CreateInstance.Create("Localize");
            return instance.ThisGetLocalizeText(key, localizeCode);
        }
        
        [SerializeField]
        private bool dontDestroyOnLoad = true;

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
        }
        
        [SerializeField]
        private List<LocalizeData> localizeData;
        
        [SerializeField]
        private List<LocalizeData> cacheData;
        
        private string ThisGetLocalizeText(string key, string localizeCode)
        {
            return key;
        }
    }
}
