using System.Collections.Generic;
using System.ComponentModel;

namespace BookViewer.Model
{
    public interface IChapter : INotifyPropertyChanged
    {
        string Title { get; }
        IList<IPage> Pages { get; }
    }
}