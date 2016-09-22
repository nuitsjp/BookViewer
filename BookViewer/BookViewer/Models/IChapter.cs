using System.Collections.Generic;

namespace BookViewer.Models
{
    public interface IChapter
    {
        int ChapterNo { get; }
        string Title { get; }
        IList<IPage> Pages { get; }
    }
}