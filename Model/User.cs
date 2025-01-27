using blog.Helper;

public enum RoleType
{
    ADMIN,
    USER
}

namespace blog.Model
{
    public class User : Base
    {
        public Guid UserId { set; get; }
        public string UserName { set; get; } = "";
        public string Password { set; get; } = "";
        public string Email { set; get; } = "";
        public RoleType Role { set; get; }
        public string? ProfilePic { set; get; }
        public string FullName { set; get; } = "";

    }
}