using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyProject_D20170331.ModelsAdd;

namespace MyProject_D20170331.Models
{
    public class OrderService
    {
        MyDB db = new MyDB();

        public List<string> GetCustName()
        {
            List<string> result = new List<string>();
            var name = db.Customers
                     .Select(x => new
                     {
                         x.CustomerID,
                         x.CompanyName
                     }).ToList();

            foreach (var item in name)
            {
                result.Add(item.CustomerID+","+item.CompanyName);
            }
            return result;
        }

        public List<string> GetEmpName()
        {
            List<string> result = new List<string>();
            var name = db.Employees
                     .Select(x => new
                     {
                         x.EmployeeID,
                         x.LastName,
                         x.FirstName
                     }).ToList();

            foreach (var item in name)
            {
                result.Add(item.EmployeeID+","+item.LastName + "-" + item.FirstName);
            }
            return result;
        } 

        public List<string> GetShipCompany()
        {
            List<string> result = new List<string>();
            var company = db.Shippers
                        .Select(x => new
                        {
                            x.ShipperID,
                            x.CompanyName
                        })
                        .ToList();

            foreach (var item in company)
            {
                result.Add(item.ShipperID+","+item.CompanyName);
            }

            return result;
        }


        public List<string> GetProduct()
        {
            List<string> result = new List<string>();
            var product = db.Products
                        .Select(x => new
                        {
                            x.ProductID,
                            x.ProductName
                        })
                        .ToList();

            foreach (var item in product)
            {
                result.Add(item.ProductID+","+item.ProductName);
            }

            return result;
        }

        public double GetProductPrice(int index)
        {

            var price = db.Products
                   .Where(x => x.ProductID == index)
                   .Select(x => new
                   {
                       x.UnitPrice
                   })
                   .ToArray();

            return Convert.ToDouble(price[0].UnitPrice);
        }

        //這裡要再改
        public List<OrderModel> GetQueryResult(int OrderID, string CompanyName, string EmployeeName, string ShipCompanyName, DateTime OrderDate, DateTime RequireDate, DateTime ShipDate)
        {
            List<OrderModel> result = new List<OrderModel>();
            DateTime emptyDate = new DateTime();

            var tmp = db.Orders
                    .Select(x => new
                    {
                        x.OrderID,
                        CompName = x.Customers.CompanyName,
                        EmployeeName = x.Employees.LastName + "-" + x.Employees.FirstName,
                        ShipCompanyName = x.Shippers.CompanyName,
                        x.OrderDate,
                        x.RequiredDate,
                        x.ShippedDate
                    })
                    .Where(x =>
                            (OrderID == 0 || x.OrderID == OrderID ) &&
                            (CompanyName == null || x.CompName.Contains(CompanyName)) &&
                            (EmployeeName == "" || x.EmployeeName == EmployeeName) &&
                            (ShipCompanyName == "" || x.ShipCompanyName == ShipCompanyName) &&
                            (OrderDate == emptyDate || x.OrderDate == OrderDate) &&
                            (RequireDate == emptyDate || x.RequiredDate == RequireDate) &&
                            (ShipDate == emptyDate || x.ShippedDate == ShipDate)
                     )
                    .OrderBy(x => x.OrderID)
                    .ToList();

            foreach (var item in tmp)
            {
                OrderModel orderModel = new OrderModel()
                {
                    OrderID = item.OrderID,
                    CustomerName = item.CompName,
                    OrderDate = item.OrderDate.ToString(),
                   ShippedDate =item.ShippedDate.ToString()
                };
                result.Add(orderModel);
            }

            return result;

        }


        public void SaveOrder(OrderModel MyOM)
        {
            DateTime? shipDate = null;
            if (MyOM.ShippedDate != null)
            {
                shipDate = Convert.ToDateTime(MyOM.ShippedDate);
            }

            Orders order = new Orders()
            {
                CustomerID = MyOM.CustomerID,
                EmployeeID = MyOM.EmployeeID,
                OrderDate = Convert.ToDateTime(MyOM.OrderDate),
                RequiredDate = Convert.ToDateTime(MyOM.RequiredDate),
                //  ShippedDate = Convert.ToDateTime(MyOM.ShippedDate
                ShippedDate = shipDate,
                ShipperID = MyOM.ShipperID,
                Freight = MyOM.Freight,
                ShipName = MyOM.ShipName,
                ShipAddress = MyOM.ShipAddress,
                ShipCity = MyOM.ShipCity,
                ShipRegion = MyOM.ShipRegion,
                ShipPostalCode = MyOM.ShipPostalCode,
                ShipCountry = MyOM.ShipCountry

            };
            db.Orders.Add(order);


            if (MyOM.ODitem != null && MyOM.ODprice !=null && MyOM.ODquantity!=null)
            {
                for (int i = 0; i < MyOM.ODitem.Length; i++)
                {
                    OrderDetails OD = new OrderDetails()
                    {
                        ProductID = MyOM.ODitem[i],
                        UnitPrice = MyOM.ODprice[i],
                        Qty = MyOM.ODquantity[i]
                    };
                    order.OrderDetails.Add(OD);
                }
            }
            db.SaveChanges();
        }



        public void DeleteQueryResult(int id)
        {
            var del_OD = db.OrderDetails
                       .Where(x => x.OrderID == id)
                       .ToList();
            db.OrderDetails.RemoveRange(del_OD);

            var del_Order = db.Orders
                           .Where(x => x.OrderID == id)
                           .ToList();
            db.Orders.RemoveRange(del_Order);

            db.SaveChanges();
        }
      

    }
}