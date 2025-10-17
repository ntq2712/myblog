
namespace blog.DTO.MyProfile
{
    public class CCaseStudyDetail
    {
        public Guid CaseStudyId { set; get; }
        public string? Title { set; get; }
        public string Content { set; get; } = "";
        public string? Thumbnail { set; get; }
        public int Index { set; get; }
    }
}