using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DTO.User
{
    public class CreateUser
    {
        public string UserName { set; get; } = "";
        public string Password { set; get; } = "";
        public string Email { set; get; } = "";
        public RoleType Role { set; get; }
        public string? ProfilePic { set; get; }
        public string FullName {set;get;} = "";
    }
}