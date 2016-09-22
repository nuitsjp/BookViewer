using System.ComponentModel;
using BookViewer.Models;
using BookViewer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookViewer.Tests.ViewModels
{
    [TestClass]
    public class TextPageViewModelTest
    {
        [TestMethod]
        public void TestCurrentPage()
        {
            var book = new Mock<IBook>();

            var vm = new TextPageViewModel(book.Object);

            Assert.IsNotNull(vm.CurrentPage);
            Assert.IsNull(vm.CurrentPage.Value);

            // ModelのCurrentPageを更新し、VMに反映されることを確認する
            var chapter = new Mock<IPage>();
            book.Setup(m => m.CurrentPage).Returns(chapter.Object);
            book.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("CurrentPage"));
            Assert.IsNotNull(vm.CurrentPage.Value);
            Assert.AreEqual(chapter.Object, vm.CurrentPage.Value);
        }

        [TestMethod]
        public void TestGoNextCommand()
        {
            var book = new Mock<IBook>();
            book.Setup(b => b.HasNext).Returns(false);

            var vm = new TextPageViewModel(book.Object);
            
            Assert.IsNotNull(vm.GoNextCommand);
            Assert.IsFalse(vm.GoNextCommand.CanExecute());

            // NasNext:false > true
            book.Setup(b => b.HasNext).Returns(true);
            book.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("HasNext"));
            Assert.IsTrue(vm.GoNextCommand.CanExecute());

            vm.GoNextCommand.Execute(null);
            book.Verify(b => b.GoNext(), Times.Once);

            // NasNext:true > false
            book.Setup(b => b.HasNext).Returns(false);
            book.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("HasNext"));
            Assert.IsFalse(vm.GoNextCommand.CanExecute());
        }


        [TestMethod]
        public void TestGoBackCommand()
        {
            var book = new Mock<IBook>();
            book.Setup(b => b.HasPrevious).Returns(false);

            var vm = new TextPageViewModel(book.Object);

            Assert.IsNotNull(vm.GoNextCommand);
            Assert.IsFalse(vm.GoBackCommand.CanExecute());

            // NasNext:false > true
            book.Setup(b => b.HasPrevious).Returns(true);
            book.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("HasPrevious"));
            Assert.IsTrue(vm.GoBackCommand.CanExecute());

            vm.GoBackCommand.Execute(null);
            book.Verify(b => b.GoBack(), Times.Once);

            // NasNext:true > false
            book.Setup(b => b.HasPrevious).Returns(false);
            book.Raise(m => m.PropertyChanged += null, new PropertyChangedEventArgs("HasPrevious"));
            Assert.IsFalse(vm.GoBackCommand.CanExecute());
        }
    }
}
