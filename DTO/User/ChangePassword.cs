
namespace blog.DTO.User
{
    public class ChangePassword
    {
        public Guid userId { set; get;}
        public string password { set; get;} = "";
    }
}