using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using BookViewer.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BookViewer.ViewModels
{
    public class TextPageViewModel : BindableBase
    {
        private readonly IBook _book;
        public ReadOnlyReactiveProperty<IPage> CurrentPage { get; }
        public ReactiveCommand GoNextCommand { get; }
        public ReactiveCommand GoBackCommand { get; }
        public TextPageViewModel(IBook book)
        {
            _book = book;
            CurrentPage = _book.ObserveProperty(b => b.CurrentPage).ToReadOnlyReactiveProperty();
            GoNextCommand = _book.ObserveProperty(b => b.HasNext).ToReactiveCommand();
            GoNextCommand.Subscribe(_ => _book.GoNext());
            GoBackCommand = _book.ObserveProperty(b => b.HasPrevious).ToReactiveCommand();
            GoBackCommand.Subscribe(_ => _book.GoBack());
        }
    }
}
