using System.ComponentModel.DataAnnotations;
using blog.Helper;

public enum SectionType
{
    TEXT = 0,
    CODE = 1,
    IMAGE = 2,
    LABEL = 3,
    DRIVER = 4,
}

namespace blog.Model
{
    public class PostSection : Base
    {
        [Key]
        public Guid PostSectionId { set; get; }
        public Guid PostId { set; get; }
        public string Image { set; get; } = "";
        public string Text { set; get; } = "";
        public int Index { set; get; }
        public SectionType Type { set; get; }
    }
}