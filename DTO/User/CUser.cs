
namespace blog.DTO.User
{
    public class CUser
    {
        public string Email { set; get; } = "";
        public string? ProfilePic { set; get; }
        public string FullName { set; get; } = "";
        public string? BirthDay { set; get; }
        public int Sex { set; get; }
        public string? Address { set; get; }
        public string? WorkingTitle { set; get; }
        public string? CompanyName { set; get; }
    }
}