using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sale.Controllers
{
    public class OrderController : Controller
    {
        /// <summary>
        /// 訂單管理系統首頁
        /// </summary>
        /// <returns></returns>
        Models.CodeService codeService = new Models.CodeService();

        public ActionResult Index()
        {
            /*List<SelectListItem> custData = new List<SelectListItem>();
            SelectList selectList = new SelectList(this, "CustomerID", "ContactName");
            ViewBag.SelectList = selectList;*/
            //Models.OrderService orderService = new Models.OrderService();
            /*var data =orderService.GetOrderById("10250");
            ViewBag.data = data.CustId + "" + data.CustName;
            return View();*/
            //Models.OrderService orderService = new Models.OrderService();
            //var order = orderService.GetOrderById("111");
            //ViewBag.CustId = order.CustId;
            //ViewBag.CustName = order.CustName;
            //ViewBag.Data = orderService.GetOrders();
            //ViewBag.custData = custData;
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.ShipCodeData = this.codeService.GetShipper();
            return View();
        }

        /// <summary>
        /// 取得訂單查詢結果
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult Index(Models.OrderSearchArg arg)
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.SearchResult = orderService.GetOrderByCondtioin(arg);

            ViewBag.ShipCodeData = this.codeService.GetShipper();
            Models.OrderService shipService = new Models.OrderService();
            ViewBag.SearchResult = shipService.GetOrderByCondtioin(arg);

            Models.OrderService orderservice = new Models.OrderService();
            Models.Order result = orderservice.GetOrderById(arg.OrderId);
            ViewBag.result = result;
            return View("Index");
        }

        /// <summary>
        /// 新增訂單的畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertOrder()
        {
            return View();
        }

        /// <summary>
        /// 新增訂單存檔的Action
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
            Models.OrderService orderService = new Models.OrderService();
            orderService.InsertOrder(order);
            return View("Index");

            return RedirectToAction("Index");
        }
        [HttpGet()]
        public JsonResult TestJson()
        {
            //var result = new Models.Order();
            //result.CustId = "123";
            //result.CustName = "abc";
            var result = new Models.Order() { CustId = "123", CustName = "abc" };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}