using System.Collections.Generic;
using System.ComponentModel;

namespace BookViewer.Model
{
    public interface IChapter : INotifyPropertyChanged
    {
        string Name { get; }
        IPage CurrentPage { get; set; }
        IList<IPage> Pages { get; set; }
    }
}