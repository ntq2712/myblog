using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using blog.Helper;

namespace blog.Model
{
    public class Comment : Base
    {
        [Key]
        public Guid CommentId {set;get;}
        public Guid PostId {set;get;}
        public string Content {set;get;} = "";
        public Guid RelyId {set;get;}
    }
}