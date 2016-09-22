using System.Linq;
using BookViewer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookViewer.Tests.Models
{
    [TestClass]
    public class ChapterTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var expected1 = new Chapter(1);
            Assert.AreEqual(1, expected1.ChapterNo);
            Assert.AreEqual("Chapter-1", expected1.Title);
            Assert.IsNotNull(expected1.Pages);
            Assert.AreEqual(Chapter.CountOfPage, expected1.Pages.Count);
            Assert.AreEqual(1, expected1.Pages.First().ChapterNo);
            Assert.AreEqual(1, expected1.Pages.First().PageNo);
            Assert.AreEqual(1, expected1.Pages.Last().ChapterNo);
            Assert.AreEqual(Chapter.CountOfPage, expected1.Pages.Last().PageNo);

            // Chapter番号を変更した場合に正しく反映されることを確認する
            var expected2 = new Chapter(2);
            Assert.AreEqual(2, expected2.ChapterNo);
            Assert.AreEqual("Chapter-2", expected2.Title);
            Assert.AreEqual(2, expected2.Pages.First().ChapterNo);
        }
    }
}
