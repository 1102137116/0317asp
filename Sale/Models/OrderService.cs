using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sale.Models
{
    /// <summary>
    ///訂單服務
    /// </summary>
    public class OrderService
    {
        /// <summary>
        /// 新增訂單
        /// </summary>
        public void InsertOrder(Models.Order order)
        {

        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        public void DeleteOrderById(String id)
        {

        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        public void UpdateOrder(Models.Order order)
        {

        }

        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <param name="id">訂單ID</param>
        /// <returns></returns>
        public Models.Order GetOrderById(string id)
        {
            Models.Order result = new Order();
            result.CustId = "123";
            result.CustName = "ABC";
            return result;
        }

        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetOrders()
        {
            List<Models.Order> result =new List<Order>();
            result.Add(new Order(){CustId ="123",CustName="高應大",EmpId=1,EmpName="王小明",Orderdate=DateTime.Parse("2016/03/21")});
            result.Add(new Order(){CustId ="456",CustName="資管系",EmpId=2,EmpName="李小華",Orderdate=DateTime.Parse("2016/03/22")});
            return result;
        }
    }
}