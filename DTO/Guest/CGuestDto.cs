using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DTO.Guest
{
    public class CGuestDto
    {
        public string FullName { set; get; } = "";
        public int Relationship { set; get; }
        public int Status { set; get; }
        public double WeddingGiftMoney { set; get; }
        public bool IsRefund { set; get; }
        public double RefundValue { set; get; }
    }
}