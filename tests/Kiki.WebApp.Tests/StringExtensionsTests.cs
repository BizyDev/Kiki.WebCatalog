namespace Kiki.WebApp.Tests
{
    using Data.Models;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        [DataRow("13", SizeFormatEnum.Simple, 13)]
        [DataRow("14", SizeFormatEnum.Simple, 14)]
        [DataRow("R16", SizeFormatEnum.Rxx, 16)]
        [DataRow("R17", SizeFormatEnum.Rxx, 17)]
        [DataRow("BlablaR18", SizeFormatEnum.Last2AlphaNumeric, 18)]
        [DataRow("Troll 19", SizeFormatEnum.Last2AlphaNumeric, 19)]
        [DataRow("185/55 R 15 82 H Quatrac 5", SizeFormatEnum.Vredestein, 15)]
        [DataRow("195/75 R 16C 107/105 R Comtrac 2 All Season", SizeFormatEnum.Vredestein, 16)]
        public void StringToSize_ShouldReturnSize_WhenInputIsOk(string text, SizeFormatEnum sizeFormat, int resultExpected)
        {
            var result = text.StringToSize(sizeFormat);
            Assert.AreEqual(resultExpected, result);
        }
    }
}
