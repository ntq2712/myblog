using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DTO.Visitor
{
    public class UpdateStatus
    {
        public Guid Id { set; get; }
        public int Status { set; get; }
    }
}