using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.IO;
using System.Text;
using GoogleSheetDownload.Editor.CSV;
using GoogleSheetDownload.Editor.Data;
using UnityEditor;
#endif
using GoogleSheetDownload.Runtime;
using StarterKit.LocalizeLib.Runtime;
using UnityEngine;

namespace StarterKit.LocalizeLib.Runtime
{
    [CreateAssetMenu(fileName = "LocalizeDatabase", menuName = "StarterKit/Localize/LocalizeDatabase")]
    public class LocalizeDatabase : RemoteDatabase<LocalizeData>
    {
#if UNITY_EDITOR
        [SerializeField] 
        private TextAsset localizeCodeGen;
        [SerializeField]
        private string localizeCodeGenNamespace = "StarterKit.LocalizeLib.Runtime";

        protected override void DownloadDatabase()
        {
            base.DownloadDatabase();
            UpdateLocalizeCodeGen();
        }

        protected override LocalizeData CreateData(SheetRowData sheetRowData)
        {
            var localizeData = new LocalizeData();
            localizeData.Key = sheetRowData.GetString("Key");
            foreach (var code in LocalizeLib.Runtime.Localize.CodeList)
            {
                var value = sheetRowData.GetString(code);
                var info = new LocalizeData.Info
                {
                    Code = code,
                    Text = value.Replace("\"", "")
                };
                localizeData.LocalizeInfos.Add(info);
            }
            return localizeData;
        }
        
        private void UpdateLocalizeCodeGen()
        {
            if (localizeCodeGen == null) return;

            var sb = new StringBuilder();
            sb.AppendLine($"namespace {localizeCodeGenNamespace}");
            sb.AppendLine("{");
            sb.AppendLine("    public static class LocalizeCodeGen");
            sb.AppendLine("    {");
            
            foreach (var data in this.Database)
            {
                var cleanKey = data.Key.Replace(" ", "_").Replace("/", "_");
                sb.AppendLine($"        public static readonly string {cleanKey} = \"{data.Key}\";");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            var path = AssetDatabase.GetAssetPath(localizeCodeGen);
            File.WriteAllText(path, sb.ToString());
            AssetDatabase.Refresh();
        }
#endif
    }
}
