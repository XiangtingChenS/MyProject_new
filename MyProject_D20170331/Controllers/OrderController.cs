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
        Models.OrderService orderService = new Models.OrderService();

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryOrder()
        {
            DropDown_EmpName();
            return View();
        }


        [HttpPost]
        public JsonResult QueryOrder(string CustomerName,int EmployeeID)
        {
            DropDown_EmpName();
            return this.Json(orderService.GetQueryResult(CustomerName, EmployeeID),JsonRequestBehavior.AllowGet);
        }


        public void DropDown_EmpName()
        {
            List<SelectListItem> emp = new List<SelectListItem>();
            var EmpName = orderService.GetEmpName();

            for (int i = 0; i < EmpName.ToArray().Length; i++)
            {
                emp.Add(new SelectListItem
                {
                    Text = EmpName[i],
                    Value = (i + 1).ToString()
                });
            }

            ViewBag.EmpName = emp;
        }

    }
}