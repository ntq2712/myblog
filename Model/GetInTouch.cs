
namespace blog.Model
{
    public class GetInTouch
    {
        public string Email { set; get; } = "";
        public string Moblie { set; get; } = "";
        public string Message { set; get; } = "";
        public Guid Id { set; get; }
        public DateTime CreateAt { set; get; }
    }
}