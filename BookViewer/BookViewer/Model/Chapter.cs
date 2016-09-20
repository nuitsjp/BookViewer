using System.Collections.Generic;
using Prism.Mvvm;

namespace BookViewer.Model
{
    public class Chapter : BindableBase, IChapter
    {
        public string Title { get; }
        public IList<IPage> Pages { get; } = new List<IPage>();

        public Chapter(int chapterNo)
        {
            Title = $"Chapter-{chapterNo}";
            for (int i = 0; i < 5; i++)
            {
                Pages.Add(new Page(chapterNo, i + 1));
            }
        }
    }
}