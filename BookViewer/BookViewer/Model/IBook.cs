using System.Collections.Generic;
using System.ComponentModel;

namespace BookViewer.Model
{
    public interface IBook : INotifyPropertyChanged
    {
        IChapter CurrentChapter { get; set; }
        IPage CurrentPage { get; }
        IList<IChapter> Chapters { get; }
        void Open();
        void GoToNextPage();
        void GoToPreviousPage();
    }
}
