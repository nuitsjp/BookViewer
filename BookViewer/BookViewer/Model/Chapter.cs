using System.Collections.Generic;
using Prism.Mvvm;

namespace BookViewer.Model
{
    public class Chapter : IChapter
    {
        public string Title { get; }
        public IList<IPage> Pages { get; }

        public Chapter(int chapterNo)
        {
            Title = $"Chapter-{chapterNo}";
            Pages = new List<IPage>();
            for (int i = 0; i < 5; i++)
            {
                Pages.Add(new Page(chapterNo, i + 1));
            }
        }
    }
}