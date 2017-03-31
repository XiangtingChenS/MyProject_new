using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject_D20170331.Controllers
{
    public class OrderController : Controller
    {
        MyProject_D20170331.Models.MyDB db = new Models.MyDB();

        // GET: Order
        public ActionResult Index()
        {
            var tmp=db.Employees.Select(x => x.Phone ).First();

            List<SelectListItem> custData = new List<SelectListItem>();
            custData.Add(new SelectListItem(){Text=tmp });

            ViewBag.custData = custData;
            ViewBag.Data = tmp;


            List<SelectListItem> emp = new List<SelectListItem>();
            Models.OrderService orderService = new Models.OrderService();
             var a= orderService.GetEmpName();

            foreach (var item in a)
            {
                emp.Add(new SelectListItem
                {
                    Text = item
                });
            }

            ViewBag.EmpName = emp;
            return View();
        }

        
    }
}