

namespace blog.DTO.MyProfile
{
    public class CWorkExperience
    {
        public string CompanyName { set; get; } = "";
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public string Introduction { set; get; } = "";
        public string Thumbnail { set; get; } = "";
        public string Title { set; get; } = "";
    }
}