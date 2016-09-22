using BookViewer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookViewer.Tests.Models
{
    [TestClass]
    public class PageTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var expected1 = new Page(1, 1);
            Assert.AreEqual(1, expected1.ChapterNo);
            Assert.AreEqual(1, expected1.PageNo);
            Assert.AreEqual("Chapter-1 Page-1", expected1.Text);

            var expected2 = new Page(2, 3);
            Assert.AreEqual(2, expected2.ChapterNo);
            Assert.AreEqual(3, expected2.PageNo);
            Assert.AreEqual("Chapter-2 Page-3", expected2.Text);
        }
    }
}
