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
        Models.OrderService orderService = new Models.OrderService();
        Models.OrderDetailService orderdetailService = new Models.OrderDetailService();
        public ActionResult Index()
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
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
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.SearchResult = orderService.GetOrderByCondtioin(arg);

            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
            
            return View("Index");
        }

        /// <summary>
        /// 新增訂單的畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertOrder()
        {
            ViewBag.CustCodeData = this.codeService.GetCustomer(-1);
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
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
                Models.OrderService orderService = new Models.OrderService();
                orderService.InsertOrder(order);
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
        public ActionResult UpdateOrder(string id)
          {
              Models.Order order = this.orderService.GetOrderById(id);
              ViewBag.OrderData = orderService.GetOrderById(id);
              ViewBag.CustCodeData = this.codeService.GetCustomer(Convert.ToInt32(order.CustId));
              ViewBag.EmpCodeData = this.codeService.GetEmp(Convert.ToInt32(order.EmpId));
              ViewBag.ShipCodeData = this.codeService.GetShipper(Convert.ToInt32(order.ShipperId));
              ViewBag.OrderDate = string.Format("{0:yyyy-MM-dd}", order.Orderdate);
              ViewBag.RequireDdate = string.Format("{0:yyyy-MM-dd}", order.RequireDdate);
              ViewBag.ShippedDate = string.Format("{0:yyyy-MM-dd}", order.ShippedDate);
              ViewBag.OrderData = order;
              return View(new Models.Order()); 
          }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateOrder(Models.Order order)
        {
            Models.OrderService orderService = new Models.OrderService();
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
            orderService.UpdateOrder(order);
            return View("Index");
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
        public ActionResult index2()
        { return View(); }
    }
}