using System.Collections.Generic;
using System.ComponentModel;

namespace BookViewer.Model
{
    public interface IChapter
    {
        string Title { get; }
        IList<IPage> Pages { get; }
    }
}