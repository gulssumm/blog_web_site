using Markdig;

namespace myLib
{
    public class MarkDown : IMarkDown
    {
        public string ConvertToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown);
        }
    }

    public interface IMarkDown
    {
        string ConvertToHtml(string markdown);
    }
}
