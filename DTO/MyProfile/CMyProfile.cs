using blog.Model;

namespace blog.DTO.MyProfile
{
    public class CMyProfile
    {
        public string FullName { set; get; } = "";
        public DateTime? BirthDay { set; get; }
        public string Introduction { set; get; } = "";
        public string Avatar { set; get;  } =  "";
        public List<MySkill> MySkills { set; get; } = [];
    }
}