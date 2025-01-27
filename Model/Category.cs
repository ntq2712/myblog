using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using blog.Helper;

namespace blog.Model
{
    public class Category : Base
    {
        [Key]
        public Guid CategoryId {set;get;}
        public string CategoryName {set;get;} = "";
        public string? Desription {set;get;}
    }
}