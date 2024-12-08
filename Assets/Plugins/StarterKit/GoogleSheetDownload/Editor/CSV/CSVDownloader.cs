using System.Collections.Generic;
using GoogleSheetDownload.Editor.Data;

namespace GoogleSheetDownload.Editor.CSV
{
    public static class CSVDownloader
    {
        /// <summary>
        /// Downloads a Google Sheet as a CSV and parses it into a list of SheetRowData objects.
        /// </summary>
        /// <param name="sheetID">The ID of the Google Sheet.</param>
        /// <param name="gid">The GID of the specific sheet within the Google Sheet.</param>
        /// <returns>A list of SheetRowData objects parsed from the CSV.</returns>
        public static List<SheetRowData> Download(string sheetID, string gid)
        {
            var url = FormatGoogleSheetLink(sheetID, gid);
            var csvString = DownloadEntry(url);
            return CSVParser.GetSheetRowDatas(csvString);
        }
        
        /// <summary>
        /// Formats the Google Sheet link for downloading as a CSV.
        /// </summary>
        /// <param name="sheetID">The ID of the Google Sheet.</param>
        /// <param name="gid">The GID of the specific sheet within the Google Sheet.</param>
        /// <param name="format">The format to download the sheet in (default is "csv").</param>
        /// <returns>The formatted Google Sheet link.</returns>
        private static string FormatGoogleSheetLink(string sheetID, string gid, string format = "csv")
        {
            return $"https://docs.google.com/spreadsheets/d/{sheetID}/export?format={format}&gid={gid}";
        }

        /// <summary>
        /// Downloads the CSV content from the given URL.
        /// </summary>
        /// <param name="url">The URL to download the CSV from.</param>
        /// <returns>The downloaded CSV content as a string.</returns>
        private static string DownloadEntry(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                string csv = client.DownloadString(url);
                return csv;
            }
        }
    }
}