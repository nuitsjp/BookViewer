using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookViewer.Model
{
    public interface IBook : INotifyPropertyChanged
    {
        IChapter CurrentChapter { get; set; }
        IPage CurrentPage { get; }
        IList<IChapter> Chapters { get; }
        void GoToNextPage();
        void GoToPreviousPage();
    }
}
