using System.Collections.Generic;
using Prism.Mvvm;

namespace BookViewer.Model
{
    public class Chapter : BindableBase, IChapter
    {
        public string Name { get; }
        public IPage CurrentPage { get; set; }
        public IList<IPage> Pages { get; set; }
    }
}