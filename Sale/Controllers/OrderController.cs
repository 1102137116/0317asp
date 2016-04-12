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
        [HttpGet()]
        public ActionResult InsertOrder()
        {
            ViewBag.CustCodeData = this.codeService.GetCustomer();
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.ShipCodeData = this.codeService.GetShipper();
            ViewBag.ProductCodeData = this.codeService.GetProduct();
            return View(new Models.Order());
        }

        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            return View(order);
            //return View();
        }

        /// <summary>
        /// 更新訂單畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult UpdateOrder()
        {
            return View();
        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateOrder(Models.Order order)
        {
            return View();
        }


        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteOrder(string orderId)
        {
            try
            {
                Models.OrderService orderService = new Models.OrderService();
                orderService.DeleteOrderById(orderId);
                return this.Json(true);
            }
            catch (Exception)
            {

                return this.Json(false);
            }
        }
    }
}