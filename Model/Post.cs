using System.ComponentModel.DataAnnotations;
using blog.Helper;

public enum StatusType {
    PublicKey = 2,
    DRAFT = 1
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
        public int ViewCount {set;get;}
        public string? CoverImage {set;get;}
    }
}