using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookViewer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookViewer.Tests.Models
{
    [TestClass]
    public class BookTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var expected = new Book();
            Assert.IsNull(expected.CurrentChapter);
            Assert.IsNull(expected.CurrentPage);
            Assert.IsNotNull(expected.Chapters);
            Assert.AreEqual(0, expected.Chapters.Count);
        }
    }
}
