
namespace blog.Helper
{
    public class Base
    {
        public Guid CreateBy {set;get;}
        public DateTime CreateAt {set;get;}
        public Guid? ModifyBy {set;get;}
        public DateTime? ModifyAt {set;get;}
        public bool IsDelete {set;get;}
    }
}