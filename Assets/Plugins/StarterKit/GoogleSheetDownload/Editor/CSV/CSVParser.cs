using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using GoogleSheetDownload.Editor.Data;
using GoogleSheetDownload.Editor.Utility;
using UnityEngine;

namespace GoogleSheetDownload.Editor.CSV
{
    public static class CSVParser
    {
        /// <summary>
        /// Splits a CSV string into lines, taking into account quoted newlines.
        /// </summary>
        /// <param name="csv">The CSV string to split.</param>
        /// <returns>A list of lines from the CSV string.</returns>
        public static List<string> SplitLine(string csv)
        {
            var lines = new List<string>();
            var currentLine = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in csv)
            {
                if (c == '\"')
                    inQuotes = !inQuotes;

                if (c == '\n' && !inQuotes)
                {
                    lines.Add(currentLine.ToString());
                    currentLine.Clear();
                }
                else
                {
                    currentLine.Append(c);
                }
            }
            
            if (currentLine.Length > 0)
                lines.Add(currentLine.ToString());

            return lines;
        }
        
        /// <summary>
        /// Splits a string by commas using a regular expression.
        /// </summary>
        /// <param name="stringValue">The string to split.</param>
        /// <returns>A list of values separated by commas.</returns>
        public static List<string> SplitComma(string stringValue)
        {
            var listItem = new List<string>();
            
            var matches = Regex.Matches(stringValue, CharacterString.RegexComma, RegexOptions.Compiled);
            foreach (var match in matches)
                listItem.Add(match.ToString());
            
            return listItem;
        }
        
        /// <summary>
        /// Splits a string by a custom separator, after clearing unwanted characters.
        /// </summary>
        /// <param name="stringValue">The string to split.</param>
        /// <param name="separator">The custom separator to use. If null, commas are used.</param>
        /// <returns>A list of values separated by the custom separator.</returns>
        public static List<string> SplitItemList(string stringValue, string separator = null)
        {
            var listItem = new List<string>();
    
            // Clear unwanted characters
            stringValue = ClearUnwantedCharacters(stringValue);
            
            // Remove double quotes
            stringValue = stringValue.Replace(CharacterString.DoubleQuotation, CharacterString.Empty);
            
            // Remove [ and ]
            stringValue = stringValue.Replace("[", CharacterString.Empty);
            stringValue = stringValue.Replace("]", CharacterString.Empty);
            
            // Replace newlines with commas
            stringValue = stringValue.Replace(CharacterString.NewLine, CharacterString.Comma);
    
            if (separator != null)
            {
                var stringArray = stringValue.Split(new string[] { separator }, System.StringSplitOptions.None);
                listItem.AddRange(stringArray);
            }
            else
            {
                var stringArray = stringValue.Split(new string[] { CharacterString.Comma }, System.StringSplitOptions.None);
                listItem.AddRange(stringArray);
            }

            return listItem;
        }

        /// <summary>
        /// Parses a CSV string into a list of SheetRowData objects.
        /// </summary>
        /// <param name="csvString">The CSV string to parse.</param>
        /// <returns>A list of SheetRowData objects.</returns>
        public static List<SheetRowData> GetSheetRowDatas(string csvString)
        {
            var sheetRowDatas = new List<SheetRowData>();

            var linesArray = CSVParser.SplitLine(csvString);
            var headerLine = CSVParser.SplitComma(linesArray[0]);
            for (int i = 1; i < linesArray.Count; i++)
            {
                var newSheetRowData = new SheetRowData();
                var cells = CSVParser.SplitComma(linesArray[i]);
                
                if (IsAllEmptyLine(cells))
                    continue;
                
                newSheetRowData.Parse(headerLine, cells);
                sheetRowDatas.Add(newSheetRowData);
            }
            
            return sheetRowDatas;
        }

        /// <summary>
        /// Clears unwanted characters from a string, such as carriage returns.
        /// </summary>
        /// <param name="lineString">The string to clear.</param>
        /// <returns>The cleaned string.</returns>
        public static string ClearUnwantedCharacters(string lineString)
        {
            var newLineString = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in lineString)
            {
                if (c == CharacterString.DoubleQuotation[0])
                {
                    inQuotes = !inQuotes;
                }

                if (!inQuotes && c == CharacterString.Return[0])
                {
                    continue;
                }

                newLineString.Append(c);
            }

            return newLineString.ToString();
        }

        /// <summary>
        /// Determines if a list of strings contains mostly empty lines.
        /// </summary>
        /// <param name="lineString">The list of strings to check.</param>
        /// <returns>True if more than half of the lines are empty, otherwise false.</returns>
        public static bool IsAllEmptyLine(List<string> lineString)
        {
            int emptyCount = 0;
    
            for (int i = 0; i < lineString.Count; i++)
            {
                var newStringLine = ClearUnwantedCharacters(lineString[i]);
                if (string.IsNullOrEmpty(newStringLine))
                    emptyCount++;
            }
    
            // If more than half of the cells are empty, it's considered an empty line
            return emptyCount > lineString.Count / 2;
        }
    }
}