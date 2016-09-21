using Prism.Mvvm;

namespace BookViewer.Models
{
    public class Page : BindableBase, IPage
    {
        public string Text { get; }

        public Page(int chapterNo, int pageNo)
        {
            Text = $"Chapter-{chapterNo} Page-{pageNo}";
        }
    }
}