using Microsoft.EntityFrameworkCore;

namespace blog.Model
{
    [Owned]
    public class UiMetadata
    {
        public string Color { set; get; } = "";
        public string ColorLight { set; get; } = "";
        public int Index { set; get; }
        public string? Icon { set; get; }
    }
}