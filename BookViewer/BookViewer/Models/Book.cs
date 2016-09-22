using System;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Mvvm;

namespace BookViewer.Models
{
    public class Book : BindableBase, IBook
    {
        public const int CountOfChapter = 20;
        private IChapter _currentChapter;
        private IPage _currentPage;

        public IChapter CurrentChapter
        {
            get { return _currentChapter; }
            set
            {
                if (SetProperty(ref _currentChapter, value))
                {
                    // Chapterを移動した場合、そのChapterの先頭ページをカレントページに設定する
                    CurrentPage = _currentChapter.Pages.First();
                }
            }
        }

        public IPage CurrentPage
        {
            get { return _currentPage; }
            private set { SetProperty(ref _currentPage, value); }
        }

        public bool HasNext
        {
            get
            {
                if (CurrentChapter == null)
                {
                    return false;
                }
                else if (CurrentChapter != Chapters.Last())
                {
                    return true;
                }
                else if (CurrentPage != CurrentChapter.Pages.Last())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasPrevious
        {
            get
            {
                if (CurrentChapter == null)
                {
                    return false;
                }
                else if (CurrentChapter != Chapters.First())
                {
                    return true;
                }
                else if (CurrentPage != CurrentChapter.Pages.First())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private readonly ObservableCollection<IChapter> _chapters = new ObservableCollection<IChapter>();
        public ReadOnlyObservableCollection<IChapter> Chapters { get; }
        public Book()
        {
            Chapters = new ReadOnlyObservableCollection<IChapter>(_chapters);
        }

        public void Open()
        {
            for (int i = 0; i < CountOfChapter; i++)
            {
                _chapters.Add(new Chapter(i + 1));
            }
            CurrentChapter = Chapters.First();
            CurrentPage = CurrentChapter.Pages.First();
            NotifyUpdatePageStatus();
        }

        public void GoNext()
        {
            if (CurrentChapter == null) throw new InvalidOperationException("This is not open.");
            if (!HasNext) throw new InvalidOperationException("Not exist next page.");

            if (CurrentPage == CurrentChapter.Pages.Last())
            {
                // 同一Chapter内の最後のページの場合
                // 次のChapterの最初のページへ
                CurrentChapter = Chapters[Chapters.IndexOf(CurrentChapter) + 1];
                CurrentPage = CurrentChapter.Pages.First();
            }
            else
            {
                // 同一Chapter内で１ページ進める
                CurrentPage = CurrentChapter.Pages[CurrentChapter.Pages.IndexOf(CurrentPage) + 1];
            }

            NotifyUpdatePageStatus();
        }

        public void GoBack()
        {
            if (CurrentChapter == null) throw new InvalidOperationException("This is not open.");
            if (!HasPrevious) throw new InvalidOperationException("Not exist previous page");

            if (CurrentPage == CurrentChapter.Pages.First())
            {
                // 同一Chapter内の最初のページの場合
                // 前のChapterの最後のページへ
                CurrentChapter = Chapters[Chapters.IndexOf(CurrentChapter) - 1];
                CurrentPage = CurrentChapter.Pages.Last();
            }
            else
            {
                // 同一Chapter内で１ページ進める
                CurrentPage = CurrentChapter.Pages[CurrentChapter.Pages.IndexOf(CurrentPage) - 1];
            }
            NotifyUpdatePageStatus();
        }

        private void NotifyUpdatePageStatus()
        {
            OnPropertyChanged(() => HasNext);
            OnPropertyChanged(() => HasPrevious);
        }
    }
}