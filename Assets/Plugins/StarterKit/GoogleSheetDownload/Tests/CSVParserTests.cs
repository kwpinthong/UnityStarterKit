using NUnit.Framework;
using System.Collections.Generic;
using GoogleSheetDownload.Editor.CSV;

namespace GoogleSheetDownload.Tests
{
    [TestFixture]
    public class CSVParserTests
    {
        [Test]
        public void SplitLine_SingleLine_ReturnsSingleLine()
        {
            var csv = "value1,value2,value3";
            var result = CSVParser.SplitLine(csv);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("value1,value2,value3", result[0]);
        }

        [Test]
        public void SplitLine_MultipleLines_ReturnsMultipleLines()
        {
            var csv = "value1,value2,value3\nvalue4,value5,value6";
            var result = CSVParser.SplitLine(csv);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("value1,value2,value3", result[0]);
            Assert.AreEqual("value4,value5,value6", result[1]);
        }

        [Test]
        public void SplitLine_QuotedNewline_ReturnsSingleLine()
        {
            var csv = "value1,\"value2\nvalue3\",value4";
            var result = CSVParser.SplitLine(csv);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("value1,\"value2\nvalue3\",value4", result[0]);
        }

        [Test]
        public void SplitComma_CommaSeparatedValues_ReturnsList()
        {
            var stringValue = "value1,value2,value3";
            var result = CSVParser.SplitComma(stringValue);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("value1", result[0]);
            Assert.AreEqual("value2", result[1]);
            Assert.AreEqual("value3", result[2]);
        }

        [Test]
        public void SplitItemList_CustomSeparator_ReturnsList()
        {
            var stringValue = "value1|value2|value3";
            var result = CSVParser.SplitItemList(stringValue, "|");
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("value1", result[0]);
            Assert.AreEqual("value2", result[1]);
            Assert.AreEqual("value3", result[2]);
        }

        [Test]
        public void GetSheetRowDatas_ValidCSV_ReturnsSheetRowDataList()
        {
            var csvString = "header1,header2\nvalue1,value2\nvalue3,value4";
            var result = CSVParser.GetSheetRowDatas(csvString);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("value1", result[0].GetString("header1"));
            Assert.AreEqual("value2", result[0].GetString("header2"));
            Assert.AreEqual("value3", result[1].GetString("header1"));
            Assert.AreEqual("value4", result[1].GetString("header2"));
        }

        [Test]
        public void ClearUnwantedCharacters_RemovesUnwantedCharacters()
        {
            var lineString = "value1\rvalue2";
            var result = CSVParser.ClearUnwantedCharacters(lineString);
            Assert.AreEqual("value1value2", result);
        }

        [Test]
        public void IsAllEmptyLine_MoreThanHalfEmpty_ReturnsTrue()
        {
            var lineString = new List<string> { "", "", "value" };
            var result = CSVParser.IsAllEmptyLine(lineString);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsAllEmptyLine_LessThanHalfEmpty_ReturnsFalse()
        {
            var lineString = new List<string> { "value1", "", "value2" };
            var result = CSVParser.IsAllEmptyLine(lineString);
            Assert.IsFalse(result);
        }
    }
}