using blog.Helper;

namespace blog.Model
{
    public class MyWorkExperience : Base
    {
        public Guid Id { set; get; }
        public string CompanyName { set; get; } = "";
        public DateTime StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public string Introduction { set; get; } = "";
        public string Thumbnail { set; get; } = "";
        public string Title { set; get; } = "";
    }
}