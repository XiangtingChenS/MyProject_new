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

        //public Models.Orders Orders = new Models.Orders();
        //public Models.OrderDetails[] OD { get; set; }
        //   public string OrderDate { get; set; }
        //  public string ShippedDate { get; set; }

        public int? CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public string OrderDate { get; set; }
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set; }
        public int ShipperID { get; set; }
        public int Freight { get; set; }
        public string ShipCountry { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipAddress { get; set; }
        public string ShipName { get; set; }

        public int[] ODitem { get; set;}
        public int[] ODprice { get; set; }
        public short[] ODquantity { get; set; }
    }
}