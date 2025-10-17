using blog.Helper;

namespace blog.Model
{
    public class MyCaseStudies : Base
    {
        public Guid Id { set; get; }
        public string Introduction { set; get; } = "";
        public List<CaseStudy> CaseStudies { set; get; } = [];
    }
}