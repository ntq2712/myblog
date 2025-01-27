using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Helper;

namespace blog.Model
{
    public class Like : Base
    {
        public Guid LikeId {set;get;}
        public Guid PostId {set;get;}
    }
}