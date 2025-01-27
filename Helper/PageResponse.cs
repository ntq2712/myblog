
namespace blog.Helper
{
    public class PageResponse
    {
        public int PageIndex {set;get;}
        public int PageSize {set;get;}
        public int TotalRow {set;get;}
        public string? SearchText {set;get;}
    }
}