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
            DropDown_ShipName();
            return View();
        }


        [HttpPost]
        public JsonResult QueryOrder(string OrderID, string CustomerName,string EmployeeName, string ShipCompanyName, string OrderDate, string RequireDate, string ShipDate)
        {
            DropDown_EmpName();
            DropDown_ShipName();

            DateTime ordereDate = new DateTime();
            DateTime shipeDate = new DateTime();
            DateTime requireDate = new DateTime();

            if (OrderID.Equals("")) OrderID = "0";
            if (!OrderDate.Equals("")) ordereDate = Convert.ToDateTime(OrderDate);
            if (!ShipDate.Equals("")) shipeDate = Convert.ToDateTime(ShipDate);
            if (!RequireDate.Equals("")) requireDate = Convert.ToDateTime(RequireDate);

            return this.Json(orderService.GetQueryResult(Convert.ToInt32(OrderID) , CustomerName, EmployeeName, ShipCompanyName, ordereDate, requireDate, shipeDate),JsonRequestBehavior.AllowGet);
        }


        public void DropDown_EmpName()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var tmp= orderService.GetEmpName();

            foreach(var item in tmp)
            {
                result.Add(new SelectListItem
                {
                    Text = item 
                });
            }
            
            ViewBag.EmpName = result;
        }


        public void DropDown_ShipName()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var tmp = orderService.GetShipCompany();

            foreach (var item in tmp)
            {
                result.Add(new SelectListItem
                {
                    Text = item
                });
            }

            ViewBag.ShipCompany = result;
        }

    }
}