using Microsoft.EntityFrameworkCore;

namespace blog.Model
{
    [Owned]
    public class MySkill
    {
        public string Name { set; get; } = "";
        public string Imange { set; get; } = "";
        public string Link { set; get; } = "";
    }
}