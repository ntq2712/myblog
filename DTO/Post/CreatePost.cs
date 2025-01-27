namespace blog.DTO.Post
{
    public class CreatePost
    {
        public string Title { set; get; } = "";
        public string? Content { set; get; }
        public StatusType Status { set; get; } = StatusType.DRAFT;
        public int ViewCount { set; get; }
        public string? CoverImage { set; get; }
    }
}