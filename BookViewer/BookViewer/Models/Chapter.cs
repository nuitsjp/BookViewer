using System.Collections.Generic;

namespace BookViewer.Models
{
    public class Chapter : IChapter
    {
        public const int CountOfPage = 5;
        public int ChapterNo { get; }
        public string Title { get; }
        public IList<IPage> Pages { get; }

        public Chapter(int chapterNo)
        {
            ChapterNo = chapterNo;
            Title = $"Chapter-{chapterNo}";
            Pages = new List<IPage>();
            for (int i = 0; i < CountOfPage; i++)
            {
                Pages.Add(new Page(chapterNo, i + 1));
            }
        }
    }
}