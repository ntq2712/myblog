using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using blog.Helper;

namespace blog.Model
{
    public class PostSection : Base
    {
        [Key]
        public Guid PostSectionId {set;get;}
        public Guid PostId {set;get;}
        public string Heading {set;get;} = "";
        public string Content {set;get;} = "";
        public int SectionOder {set;get;}
    }   
}