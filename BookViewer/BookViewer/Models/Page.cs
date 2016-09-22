using Prism.Mvvm;

namespace BookViewer.Models
{
    public class Page : IPage
    {
        public int ChapterNo { get; }
        public int PageNo { get; }
        public string Text { get; }

        public Page(int chapterNo, int pageNo)
        {
            ChapterNo = chapterNo;
            PageNo = pageNo;
            Text = $"Chapter-{chapterNo} Page-{pageNo}";
        }
    }
}