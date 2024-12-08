using System.Collections.Generic;
using System.Linq;
using GoogleSheetDownload.Editor.CSV;
using UnityEngine;

namespace GoogleSheetDownload.Editor.Data
{
    public class SheetRowData
    {
        public List<KeyContainer> Columns;
        
        public SheetRowData()
        {
            Columns = new List<KeyContainer>();
        }

        public void Parse(List<string> headerLine, List<string> cells, int startColumnIndex = 0)
        {
            Columns.Clear();
            for (var i = startColumnIndex; i < headerLine.Count; i++)
            {
                Columns.Add(new KeyContainer
                {
                    Header = CSVParser.ClearUnwantedCharacters(headerLine[i]),
                    CellValue = cells[i],
                });
            }
        }

        public int GetInt(string header)
        {
            foreach (var column in Columns)
            {
                if (column.Header == header)
                    return int.TryParse(column.CellValue, out var result) ? result : -1;
            }
            
            return -1;
        }

        public float GetFloat(string header)
        {
            foreach (var column in Columns)
            {
                if (column.Header == header)
                    return float.TryParse(column.CellValue, out var result) ? result : -1;
            }
            
            return -1;
        }
        
        public string GetString(string header)
        {
            foreach (var column in Columns)
            {
                if (column.Header == header)
                    return column.CellValue;
            }
            
            return string.Empty;
        }
        
        public bool GetBool(string header)
        {
            foreach (var column in Columns)
            {
                if (column.Header == header)
                    return bool.TryParse(column.CellValue, out var result) ? result : false;
            }
            
            return false;
        }

        public List<T> GetList<T>(string header, string separator = null)
        {
            foreach (var column in Columns)
            {
                if (column.Header == header)
                    return CSVParser.SplitItemList(column.CellValue, separator).Cast<T>().ToList<T>();
            }
            
            return new List<T>();
        }
    }
}
