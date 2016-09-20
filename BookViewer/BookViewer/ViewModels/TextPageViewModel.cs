using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BookViewer.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BookViewer.ViewModels
{
    public class TextPageViewModel : BindableBase
    {
        public string Test { get; } = "test";
        private IBook _book;
        public ReactiveProperty<IPage> CurrentPage { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public TextPageViewModel(IBook book)
        {
            _book = book;
            CurrentPage = _book.ObserveProperty(b => b.CurrentPage).ToReactiveProperty();
            NextPageCommand = new DelegateCommand(() => _book.GoToNextPage());
            PreviousPageCommand = new DelegateCommand(() => _book.GoToPreviousPage());
        }
    }
}
