using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Model
{
    public class PostCategory
    {
        [Key]
        public Guid PostId { set; get; }
        public Guid CategoryId { set; get;}
    }
}