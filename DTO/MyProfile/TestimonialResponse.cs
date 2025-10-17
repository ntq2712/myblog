
using blog.Model;

namespace blog.DTO.MyProfile
{
    public class TestimonialResponse
    {
        public Guid Id { set; get; }
        public string Content { set; get; } = "";
        public string? WorkingTitle { set; get; }
        public string? CompanyName { set; get; }
        public string FullName { set; get; } = "";
        public string? ProfilePic { set; get; }

    }
}