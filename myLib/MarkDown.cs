using Markdig;

namespace myLib
{
    public class MarkDown
    {
        public static string ConvertToHtml(string markdown)
        {
            return Markdown.ToHtml(markdown);
        }
    }
}
