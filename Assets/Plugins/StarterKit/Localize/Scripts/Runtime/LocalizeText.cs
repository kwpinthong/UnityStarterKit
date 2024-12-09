using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StarterKit.Common;
#if UNITY_EDITOR
using StarterKit.Common.Editor;
#endif
using UnityEngine;
using TMPro;

namespace StarterKit.LocalizeLib.Runtime
{
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField]
        private LocalizeDatabase database;
        
#if UNITY_EDITOR
        [ValueDropdown("LocalizeCodeList")]
#endif
        [SerializeField] 
        private string key;

        [SerializeField] 
        private TMP_Text textTMP;

        private void OnEnable()
        {
            if (string.IsNullOrEmpty(key))
            {
                CommonDebug.LogError("LocalzieKey is empty", gameObject);
                return;
            }
            textTMP.text = Localize.GetLocalizeText(key);
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            textTMP = GetComponent<TMPro.TMP_Text>();
            database = AssetFinder.Find<LocalizeDatabase>("LocalizeDatabase");
        }

        private List<string> LocalizeCodeList()
        {
            var localizeList = new List<string>();
            if (database != null)
            {
                var databaseCount = database.Count;
                for (var i = 0; i < databaseCount; i++)
                {
                    var data = database[i];
                    localizeList.Add(data.Key);
                }
            }
            return localizeList;
        }
#endif
    }
}