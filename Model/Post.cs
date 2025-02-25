using System.ComponentModel.DataAnnotations;
using blog.Helper;

public enum StatusType {
    PUBLIC = 2,
    DRAFT = 1,
    REJECT = 3
}

public enum PostType {
    FREE = 0,
    PREMIUM = 1,
    GROUP = 3
}

namespace blog.Model
{
    public class Post : Base
    {
        [Key]
        public Guid PostId { set; get; }
        public string Title { set; get; } = "";
        public string? Content {set;get;}
        public StatusType Status {set;get;} = StatusType.DRAFT;
        public int TotalViews {set;get;}
        public int TotalLikes {set;get;}
        public int TotalComments {set;get;}
        public string? CoverImage {set;get;}
        public Guid CategoryId {set;get;}
        public PostType Type {set;get;}
    }
}