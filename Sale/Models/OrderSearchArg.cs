using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sale.Models
{
    public class OrderSearchArg
    {
        public string OrderId { get; set; }
        public string CustName { get; set; }
        public string OrderDate { get; set; }
        public int EmpId { get; set; }
        public int ShipperID { get; set; }
        public string ShippedDate { get; set; }
        public string RequireDdate { get; set; }
        public string DeleteOrderId { get; set; }
    }
}