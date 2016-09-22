using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookViewer.Models;
using BookViewer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookViewer.Tests.ViewModels
{
    [TestClass]
    public class ContentsPageViewModelTest
    {
        [TestMethod]
        public void TestChapters()
        {
            // Set up Mock
            var book = new Mock<IBook>();
            var chapters = new ObservableCollection<IChapter>();
            book.Setup(b => b.Chapters).Returns(new ReadOnlyObservableCollection<IChapter>(chapters));

            var vm = new ContentsPageViewModel(book.Object);

            Assert.IsNotNull(vm.Chapters);
            Assert.AreEqual(0, vm.Chapters.Count);

            var chapter = new Mock<IChapter>();
            chapters.Add(chapter.Object);
            Assert.AreEqual(1, vm.Chapters.Count);
            Assert.AreEqual(chapter.Object, vm.Chapters.First());
        }

        [TestMethod]
        public void TestCurrentChapter()
        {
            // Set up Mock
            var book = new Mock<IBook>();
            book.Setup(b => b.Chapters).Returns(new ReadOnlyObservableCollection<IChapter>(new ObservableCollection<IChapter>()));

            var vm = new ContentsPageViewModel(book.Object);

            Assert.IsNotNull(vm.CurrentChapter);
            Assert.IsNull(vm.CurrentChapter.Value);

            // ModelのCurrentChapterが更新された倍に、VMのCurrentChapterが更新されることを確認する
            var chapter1 = new Mock<IChapter>();
            book.Setup(b => b.CurrentChapter).Returns(chapter1.Object);
            book.Raise(b => b.PropertyChanged += null, new PropertyChangedEventArgs("CurrentChapter"));
            Assert.AreEqual(chapter1.Object, vm.CurrentChapter.Value);

            // VMのCurrentChapterを更新した場合に、ModelのCurrentChapterに値が設定されることを確認する
            var chapter2 = new Mock<IChapter>();
            IChapter updateChapter = null;
            book.SetupSet(m => m.CurrentChapter = It.IsAny<IChapter>())
                .Callback<IChapter>(c => updateChapter = c);
            vm.CurrentChapter.Value = chapter2.Object;
            Assert.IsNotNull(updateChapter);
            Assert.AreEqual(chapter2.Object, updateChapter);
        }
    }
}
