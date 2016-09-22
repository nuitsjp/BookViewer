using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BookViewer.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BookViewer.ViewModels
{
    public class ContentsPageViewModel : BindableBase
    {
        private readonly IBook _book;
        public ReadOnlyReactiveCollection<IChapter> Chapters { get; }
        public ReactiveProperty<IChapter> CurrentChapter { get; }
        public ContentsPageViewModel(IBook book)
        {
            _book = book;
            Chapters = _book.Chapters.ToReadOnlyReactiveCollection();
            CurrentChapter = _book.ToReactivePropertyAsSynchronized(b => b.CurrentChapter);
        }
    }
}
