using Prism.Mvvm;
using BookViewer.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace BookViewer.ViewModels
{
    public class ContentsPageViewModel : BindableBase
    {
        public ReadOnlyReactiveCollection<IChapter> Chapters { get; }
        public ReactiveProperty<IChapter> CurrentChapter { get; }
        public ContentsPageViewModel(IBook book)
        {
            Chapters = book.Chapters.ToReadOnlyReactiveCollection();
            CurrentChapter = book.ToReactivePropertyAsSynchronized(b => b.CurrentChapter);
        }
    }
}
