using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DTO.PostSection
{
    public class CreatePostSection
    {
        [Required]
        public Guid PostId { set; get; }
        [Required]
        public string Heading { set; get; } = "";
        public string Content { set; get; } = "";
        public int SectionOder { set; get; }
    }
}