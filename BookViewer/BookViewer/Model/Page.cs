using Prism.Mvvm;

namespace BookViewer.Model
{
    public class Page : BindableBase, IPage
    {
        public string Text { get; }
    }
}