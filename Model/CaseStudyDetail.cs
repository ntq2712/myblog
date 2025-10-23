
using blog.Helper;

namespace blog.Model
{
    public class CaseStudyDetail : Base
    {
        public Guid Id { set; get; }
        public Guid CaseStudyId { set; get; }
        public string? Title { set; get; }
        public string? Content { set; get; }
        public string? Thumbnail { set; get; } = "";
        public int Index { set; get; }
    }
}