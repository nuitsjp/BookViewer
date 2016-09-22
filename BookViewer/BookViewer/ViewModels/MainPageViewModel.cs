using System.Windows.Input;
using BookViewer.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace BookViewer.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IBook _book;
        public ICommand OpenCommand { get; }
        public MainPageViewModel(IBook book)
        {
            _book = book;
            OpenCommand = new DelegateCommand(() => _book.Open());
        }
    }
}
