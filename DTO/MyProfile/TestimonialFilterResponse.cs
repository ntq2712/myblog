using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DTO.MyProfile
{
    public class TestimonialFilterResponse
    {
        public Guid Id { set; get; }
        public string Content { set; get; } = "";
        public string? WorkingTitle { set; get; }
        public string? CompanyName { set; get; }
        public string FullName { set; get; } = "";
        public string? ProfilePic { set; get; }
        public int Status { set; get; }
    }
}