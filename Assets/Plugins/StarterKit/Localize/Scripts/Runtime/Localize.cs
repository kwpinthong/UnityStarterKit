using System;
using System.Collections;
using System.Collections.Generic;
using StarterKit.Common;
using StarterKit.LocalizeLib.Runtime;
using UnityEngine;

namespace StarterKit.LocalizeLib.Runtime
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
        private LocalizeDatabase database;
        
        [SerializeField]
        private List<LocalizeData> cacheLocalizeData;
        
        private string ThisGetLocalizeText(string key, string localizeCode)
        {
            var data = cacheLocalizeData.Find(i => i.Key == key);
            if (data != null)
            {
                return data.LocalizeInfos.Find(info => info.Code == localizeCode).Text;
            }
            data = database.Find(i => i.Key == key);
            if (data != null)
            {
                cacheLocalizeData.Add(data);
                return data.LocalizeInfos.Find(info => info.Code == localizeCode).Text;
            }
            return key;
        }
    }
}
