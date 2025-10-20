
namespace blog.DTO.Visitor
{
    public class VisitorResponse
    {
        public Guid Id { set; get; }
        public string FullName { set; get; } = "";
        public string CompanyName { set; get; } = "";
        public string PositionTitle { set; get; } = "";
        public string Avatar { set; get; } = "";
        public string? Email { set; get; }
    }
}