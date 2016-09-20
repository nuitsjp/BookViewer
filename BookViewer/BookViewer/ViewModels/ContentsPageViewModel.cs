using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BookViewer.Model;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BookViewer.ViewModels
{
    public class ContentsPageViewModel : BindableBase
    {
        private readonly IBook _book;
        public IList<IChapter> Chapters { get; }
        public ReactiveProperty<IChapter> CurrentChapter { get; }
        public ICommand OpenCommand { get; }
        public ContentsPageViewModel(IBook book)
        {
            _book = book;
            Chapters = _book.Chapters.ToList();
            CurrentChapter = _book.ToReactivePropertyAsSynchronized(b => b.CurrentChapter);
            OpenCommand = new DelegateCommand(() => _book.Open());
        }
    }
}
