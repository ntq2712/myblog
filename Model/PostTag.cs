using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Model
{
    public class PostTag
    {
        [Key]
        public Guid PostTagId {set;get;}
        public Guid PostId {set;get;}
        public Guid TagId { set;get;}
    }
}