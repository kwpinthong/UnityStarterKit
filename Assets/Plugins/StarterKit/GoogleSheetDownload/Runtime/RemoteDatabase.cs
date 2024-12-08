using System;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
using NaughtyAttributes;
#endif
using UnityEngine;
#if UNITY_EDITOR
using GoogleSheetDownload.Editor;
using GoogleSheetDownload.Editor.CSV;
using GoogleSheetDownload.Editor.Data;
using UnityEditor;
#endif

namespace GoogleSheetDownload.Runtime
{
    public abstract class RemoteDatabase<T> : ScriptableObject
    {
        #region Runtime

        [SerializeField] protected List<T> Database = new List<T>();

        public int Count => Database.Count;
        
        public T this[int index] => Database[index];
        
        public T Find(Predicate<T> match)
        {
            return Database.Find(match);
        }
        
        #endregion
        
        #region Editor
        
#if UNITY_EDITOR
        
        [Header("Remote Download")]
        [SerializeField] protected string sheetID;
        [SerializeField] protected List<RemoteEnty> enties = new();
        
        [Button]
        protected virtual void DownloadDatabase()
        {
            Database.Clear();
            
            foreach (var enty in enties)
            {
                var sheetRowDatas = CSVDownloader.Download(sheetID, enty.GID);

                for (int i = 0; i < sheetRowDatas.Count; i++)
                {
                    try
                    {
                        var newData = CreateData(sheetRowDatas[i]);
                        Database.Add(newData);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
                
                Debug.Log("Complete Download: " + enty.TabName);
            }
            
            EditorUtility.SetDirty(this); 
            AssetDatabase.Refresh();
        }

        protected abstract T CreateData(SheetRowData sheetRowData);

#endif
        
        #endregion
    }
}
