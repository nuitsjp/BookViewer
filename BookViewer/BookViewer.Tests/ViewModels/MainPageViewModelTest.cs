using BookViewer.Models;
using BookViewer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookViewer.Tests.ViewModels
{
    [TestClass]
    public class MainPageViewModelTest
    {
        [TestMethod]
        public void TestOpenCommand()
        {
            var book = new Mock<IBook>();
            var vm = new MainPageViewModel(book.Object);
            
            Assert.IsTrue(vm.OpenCommand.CanExecute(null));

            vm.OpenCommand.Execute(null);
            book.Verify(b => b.Open(), Times.Once);
        }
    }
}
