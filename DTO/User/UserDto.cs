
namespace blog.DTO.User
{
    public class UserDto
    {
        public Guid UserId { set; get; }
        public string Email { set; get; } = "";
        public RoleType Role { set; get; }
        public string? ProfilePic { set; get; }
        public string FullName { set; get; } = "";
        public DateTime CreateAt {set;get;}
        public Guid? ModifyBy {set;get;}
        public DateTime? ModifyAt {set;get;}
    }
}