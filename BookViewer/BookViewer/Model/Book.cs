using Prism.Mvvm;
using System.Collections.Generic;

namespace BookViewer.Model
{
    public class Book : BindableBase, IBook
    {
        public IChapter CurrentChapter { get; set; }
        public IList<IChapter> Chapters { get; }
    }
}