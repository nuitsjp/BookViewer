using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BookViewer.Model
{
    public interface IBook : INotifyPropertyChanged
    {
        IChapter CurrentChapter { get; set; }
        IPage CurrentPage { get; }
        ReadOnlyObservableCollection<IChapter> Chapters { get; }
        void Open();
        void GoToNextPage();
        void GoToPreviousPage();
    }
}
