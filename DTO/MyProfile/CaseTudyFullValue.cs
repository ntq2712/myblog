using blog.Model;

namespace blog.DTO.MyProfile
{
    public class CaseTudyFullValue : CaseStudy
    {
        public CaseStudyType CaseStudyType { set; get; } = new CaseStudyType();
    }
}