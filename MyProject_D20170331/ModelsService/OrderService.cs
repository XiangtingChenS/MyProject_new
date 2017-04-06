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
                    OrderDate = item.OrderDate.Date.ToString(),
                    ShippedDate = item.ShippedDate.ToString()
                };
                result.Add(orderModel);
            }

            return result;

        }


        public void SaveOrder(Models.Orders order)
        {
            Orders o = new Orders()
            {
                CustomerID=order.CustomerID,
                EmployeeID=order.EmployeeID,
                OrderDate=order.OrderDate,
                RequiredDate=order.RequiredDate,
                ShippedDate=order.ShippedDate,
                ShipperID=order.ShipperID,
                Freight=order.Freight,
                ShipName=order.ShipName,
                ShipAddress=order.ShipAddress,
                ShipCity=order.ShipCity,
                ShipRegion=order.ShipRegion,
                ShipPostalCode=order.ShipPostalCode,
                ShipCountry=order.ShipCountry

            };
       //     db.Orders.Add(o);

        //    db.SaveChanges();
        }

    }
}