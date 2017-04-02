using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject_D20170331.ModelsAdd
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string OrderDate { get; set; }
        public string ShippedDate { get; set; }
    }
}