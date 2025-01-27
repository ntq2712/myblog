using System.ComponentModel.DataAnnotations;
using blog.Helper;

namespace blog.Model
{
    public class Tag : Base
    {
        [Key]
        public Guid TagId {set;get;}
        public string TagName {set;get;} = "";
    }
}