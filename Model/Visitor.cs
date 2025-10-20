using blog.Helper;

namespace blog.Model
{
    public class Visitor : Base
    {
        public Guid Id { set; get; }
        public string FullName { set; get; } = "";
        public string CompanyName { set; get; } = "";
        public string PositionTitle { set; get; } = "";
        public string Avatar { set; get; } = "";
        public string? Email { set; get; }
        public string Account { set; get; } = "";
        public string Password { set; get; } = "";
    }
}