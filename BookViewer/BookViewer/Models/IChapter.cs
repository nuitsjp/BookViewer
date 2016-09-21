using System.Collections.Generic;

namespace BookViewer.Models
{
    public interface IChapter
    {
        string Title { get; }
        IList<IPage> Pages { get; }
    }
}