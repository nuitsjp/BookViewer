using System;
using System.Linq;
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
            Assert.IsFalse(expected.HasNext);
            Assert.IsFalse(expected.HasPrevious);
            Assert.IsNotNull(expected.Chapters);
            Assert.AreEqual(0, expected.Chapters.Count);
        }

        [TestMethod]
        public void TestOpem()
        {
            var book = new Book();
            // has～が、正しく通知されるかどうか判定するためイベントを監視する
            bool hasNext = false;
            bool hasPrevious = false;
            IChapter currentChapter = null;
            IPage currentPage = null;
            book.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "HasNext")
                {
                    hasNext = book.HasNext;
                }
                else if (args.PropertyName == "HasPrevious")
                {
                    hasPrevious = book.HasPrevious;
                }
                else if (args.PropertyName == "CurrentChapter")
                {
                    currentChapter = book.CurrentChapter;
                }
                else if (args.PropertyName == "CurrentPage")
                {
                    currentPage = book.CurrentPage;
                }
            };

            book.Open();

            Assert.AreEqual(Book.CountOfChapter, book.Chapters.Count);
            Assert.IsTrue(hasNext);
            Assert.IsFalse(hasPrevious);
            Assert.IsNotNull(currentChapter);
            Assert.AreEqual(book.Chapters.First(), currentChapter);
            Assert.IsNotNull(currentPage);
            Assert.AreEqual(book.CurrentChapter.Pages.First(), currentPage);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGoNextBeforeOpen()
        {
            var book = new Book();
            book.GoNext();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGoBackBeforeOpen()
        {
            var book = new Book();
            book.GoBack();
        }

        [TestMethod]
        public void TestMovePage()
        {
            var book = new Book();

            // has～が、正しく通知されるかどうか判定するためイベントを監視する
            bool hasNext = false;
            bool hasPrevious = false;
            IChapter currentChapter = null;
            IPage currentPage = null;
            book.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "HasNext")
                {
                    hasNext = book.HasNext;
                }
                else if (args.PropertyName == "HasPrevious")
                {
                    hasPrevious = book.HasPrevious;
                }
                else if (args.PropertyName == "CurrentChapter")
                {
                    currentChapter = book.CurrentChapter;
                }
                else if (args.PropertyName == "CurrentPage")
                {
                    currentPage = book.CurrentPage;
                }
            };

            // Bookを開く
            book.Open();

            // 1-1 > 1-2
            book.GoNext();
            Assert.IsTrue(hasNext);
            Assert.IsTrue(hasPrevious);
            Assert.AreEqual(book.Chapters.First(), currentChapter);
            Assert.AreEqual(currentChapter.Pages[1], currentPage);

            // 1-2 > 1-1
            book.GoBack();
            Assert.IsTrue(hasNext);
            Assert.IsFalse(hasPrevious);
            Assert.AreEqual(book.Chapters.First(), currentChapter);
            Assert.AreEqual(currentChapter.Pages.First(), currentPage);

            // 1-1 > 2-1
            book.GoNext();
            book.GoNext();
            book.GoNext();
            book.GoNext();
            book.GoNext();
            Assert.IsTrue(hasNext);
            Assert.IsTrue(hasPrevious);
            Assert.AreEqual(book.Chapters[1], currentChapter);
            Assert.AreEqual(currentChapter.Pages.First(), currentPage);

            // 2-1 > 1-5
            book.GoBack();
            Assert.IsTrue(hasNext);
            Assert.IsTrue(hasPrevious);
            Assert.AreEqual(book.Chapters.First(), currentChapter);
            Assert.AreEqual(currentChapter.Pages.Last(), currentPage);

            // 1-5 > 20-1
            book.CurrentChapter = book.Chapters.Last();
            Assert.IsTrue(hasNext);
            Assert.IsTrue(hasPrevious);
            Assert.AreEqual(book.Chapters.Last(), currentChapter);
            Assert.AreEqual(currentChapter.Pages.First(), currentPage);

            // 20-1 > 20-5
            book.GoNext();
            book.GoNext();
            book.GoNext();
            book.GoNext();
            Assert.IsFalse(hasNext);
            Assert.IsTrue(hasPrevious);
            Assert.AreEqual(book.Chapters.Last(), currentChapter);
            Assert.AreEqual(currentChapter.Pages.Last(), currentPage);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestNotExistPresiousPage()
        {
            var book = new Book();
            book.Open();
            book.GoBack();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestNotExistNextPage()
        {
            var book = new Book();
            book.Open();
            book.CurrentChapter = book.Chapters.Last();
            book.GoNext();
            book.GoNext();
            book.GoNext();
            book.GoNext();
            book.GoNext();
        }

    }
}
