using blog.Helper;

namespace blog.Model
{
    public class CaseStudy : Base
    {
        public Guid Id { set; get; }
        public string Introduction { set; get; } = "";
        public Guid Type { set; get; }
        public string Thumbnail { set; get; } = "";
        public string Title { set; get; } = "";
        public bool Secret { set; get; } = false;
    }
}