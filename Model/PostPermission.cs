using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Model
{
    public class PostPermission
    {
        [Key]
        public Guid PostPermissionId { set; get; }
        public Guid PostId { set; get; }
        public Guid UserId { set; get; }
        public int Permission { set; get; }
    }
}