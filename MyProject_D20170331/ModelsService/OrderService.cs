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

        public List<string> GetEmpName()
        {
            List<string> emp = new List<string>();
            var name = db.Employees
                     .Select(x => new
                     {
                         x.LastName,
                         x.FirstName
                     }).ToList();

            foreach (var item in name)
            {
                emp.Add(item.LastName+"-"+item.FirstName);
            }

            return emp;
        }

        public List<OrderModel> GetQueryResult(string CompanyName, int EmployeeID)
        {
            List<OrderModel> result = new List<OrderModel>();

            var tmp = db.Orders
                    .Select(x => new
                    {
                        x.OrderID,
                        CompName = x.Customers.CompanyName,
                        x.OrderDate,
                        x.ShippedDate,
                        x.EmployeeID
                    })
                    .Where(x => x.CompName == CompanyName && x.EmployeeID==EmployeeID)
                    .OrderBy(x=>x.OrderID)
                    .ToList();

            foreach (var item in tmp)
            {
                OrderModel orderModel = new OrderModel()
                {
                    OrderID = item.OrderID,
                    CustomerName=item.CompName,
                    OrderDate=item.OrderDate.Date.ToString(),
                    ShippedDate=item.ShippedDate.ToString()
                };
                result.Add(orderModel);
            }

            return result;

        }


    }
}