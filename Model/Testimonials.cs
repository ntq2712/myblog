using blog.Helper;

namespace blog.Model
{
    public class Testimonials : Base
    {
        public Guid Id { set; get; }
        public string Content { set; get; } = "";
        public Guid UserId { set; get; }
        public int Status { set; get; }
    }
}