using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DTO.MyProfile
{
    public class CCaseStudy
    {
        public string Introduction { set; get; } = "";
        public Guid Type { set; get; }
        public string Thumbnail { set; get; } = "";
        public string Title { set; get; } = "";
    }
}