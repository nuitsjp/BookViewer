namespace BookViewer.Models
{
    public interface IPage
    {
        int ChapterNo { get; }
        int PageNo { get; }
        string Text { get; }
    }
}