using blog.Helper;

namespace blog.Model
{
    public class MyProfile : Base
    {
        public Guid Id { set; get; }
        public string FullName { set; get; } = "";
        public DateTime? BirthDay { set; get; }
        public string Introduction { set; get; } = "";
        public string Avatar { set; get; } = "";
        public List<MySkill> MySkills { set; get; } = [];
    }
}