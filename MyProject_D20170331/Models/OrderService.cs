using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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


    }
}