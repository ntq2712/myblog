using blog.Model;

namespace blog.DTO.MyProfile
{
    public class CaseStudyDetailById
    {
        public Guid Id { set; get; }
        public string Despription { set; get; } = "";
        public string Thumbnail { set; get; } = "";
        public string Title { set; get; } = "";
        public CaseStudyType CaseStudyType { set; get; } = new CaseStudyType();
        public List<CaseStudyDetail>? Details { set; get; }
    }
}