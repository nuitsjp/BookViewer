using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BookViewer.Models
{
    public interface IBook : INotifyPropertyChanged
    {
        IChapter CurrentChapter { get; set; }
        IPage CurrentPage { get; }
        bool HasNext { get; }
        bool HasPrevious { get; }
        ReadOnlyObservableCollection<IChapter> Chapters { get; }
        void Open();
        void GoNext();
        void GoBack();
    }
}
