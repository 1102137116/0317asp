using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sale.Models
{
    public class OrderSearchArg
    {
        public int OrderId { get; set; }
        public string CustName { get; set; }
        public string OrderDate { get; set; }
        public string EmpId { get; set; }
        public string ShipperId { get; set; }
        public string DeleteOrderId { get; set; }
    }
}