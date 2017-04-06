using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject_D20170331.Controllers
{
    public class OrderController : Controller
    {
        Models.MyDB db = new Models.MyDB();
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


        public ActionResult InsertOrder()
        {
            DropDown_EmpName();
            DropDown_ShipName();
            DropDown_CustName();
            return View();
        }

        [HttpPost]
        public void InsertOrder(Models.Orders order)
        {
            orderService.SaveOrder(order);
        }

        public JsonResult getProduct()
        {
            return this.Json(orderService.GetProduct(),JsonRequestBehavior.AllowGet);
        }

        public void DropDown_EmpName()
        {
            ViewBag.EmpName = getResult(orderService.GetEmpName().ToArray());
        }
        
        public void DropDown_ShipName()
        {
            ViewBag.ShipCompany = getResult(orderService.GetShipCompany().ToArray());
        }

        public void DropDown_CustName()
        {
            ViewBag.CustName = getResult(orderService.GetCustName().ToArray());
        }

        public List<SelectListItem> getResult(String[] listStr)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            string value="";
            string[] MySplit= { };
            for (int i = -1; i < listStr.Length; i++)
            {
                if (i != -1)
                {
                     MySplit = listStr[i].Split(',');
                     value = MySplit[0];
                }
                else
                {
                    value = "0";
                }
               
                string text = (i == -1) ? text = string.Empty : text = MySplit[1];

                result.Add(new SelectListItem
                {
                    Value = value,
                    Text = text
                });
            }

            return result;
        }

    }
}