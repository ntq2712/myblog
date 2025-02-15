
namespace blog.DTO.User
{
    public class CreateUser
    {
        public string UserName { set; get; } = "";
        public string Password { set; get; } = "";
        public string Email { set; get; } = "";
        public string? ProfilePic { set; get; }
        public string FullName {set;get;} = "";
    }
}