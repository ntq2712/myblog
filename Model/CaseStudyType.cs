using blog.Helper;

namespace blog.Model
{
    public class CaseStudyType : Base
    {
        public Guid Id { set; get; }
        public string Name { set; get; } = "";
        public UiMetadata UiMetadata { set; get; } = new UiMetadata();
    }
}