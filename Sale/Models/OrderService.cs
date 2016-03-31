using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sale.Models
{
    /// <summary>
    ///訂單服務
    /// </summary>
    public class OrderService
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

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
        /// 依照Id 取得訂單資料
        /// </summary>
        /// <returns></returns>
        public Models.Order GetOrderById(int orderId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT 
					A.OrderId,A.CustomerID,B.Companyname As CustName,
					A.EmployeeID,C.lastname+ C.firstname As EmpName,
					A.Orderdate,A.RequireDdate,A.ShippedDate,
					A.ShipperID,D.companyname As ShipperName,A.Freight,
					A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry
					From Sales.Orders As A 
					INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
					INNER JOIN HR.Employees As C On A.EmployeeID=C.EmployeeID
					inner JOIN Sales.Shippers As D ON A.ShipperID=D.ShipperID
					Where  A.OrderId=@OrderId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", orderId));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapOrderDataToList(dt).FirstOrDefault();
        }

        /// <summary>
        /// 依照條件取得訂單資料
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetOrderByCondtioin(Models.OrderSearchArg arg)
        {

            DataTable dt = new DataTable();
            string sql = @"SELECT 
					A.OrderId,A.CustomerID,B.Companyname As CustName,
					A.EmployeeID,C.lastname+ C.firstname As EmpName,
					A.Orderdate,A.RequireDdate,A.ShippedDate,
					A.ShipperID,D.companyname As ShipperName,A.Freight,
					A.ShipName,A.ShipAddress,A.ShipCity,A.ShipRegion,A.ShipPostalCode,A.ShipCountry
					From Sales.Orders As A 
					INNER JOIN Sales.Customers As B ON A.CustomerID=B.CustomerID
					INNER JOIN HR.Employees As C On A.EmployeeID=C.EmployeeID
					inner JOIN Sales.Shippers As D ON A.ShipperID=D.ShipperID
					Where (B.Companyname Like @CustName Or @CustName='') AND 
						  (A.OrderId=@OrderId Or @OrderId='') AND
                          (A.EmployeeID=@EmployeeID Or @EmployeeID='') AND
                          (A.ShipperID=@ShipperID Or @ShipperID='') AND
                          (A.ShippedDate=@ShippedDate Or @ShippedDate='') AND
						  (A.Orderdate=@Orderdate Or @Orderdate='')AND
						  (A.RequireDdate=@RequireDdate Or @RequireDdate='')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustName", arg.CustName == null ? string.Empty : arg.CustName));
                cmd.Parameters.Add(new SqlParameter("@OrderId", arg.OrderId == null ? default(int) : arg.OrderId));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", arg.EmpId == null ? default(int) : arg.EmpId));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", arg.ShipperID == null ? default(int) : arg.ShipperID));
                cmd.Parameters.Add(new SqlParameter("@Orderdate", arg.OrderDate == null ? string.Empty : arg.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", arg.ShippedDate == null ? string.Empty : arg.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@RequireDdate", arg.RequireDdate == null ? string.Empty : arg.RequireDdate));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }


            return this.MapOrderDataToList(dt);
        }

        private List<Models.Order> MapOrderDataToList(DataTable orderData)
        {
            List<Models.Order> result = new List<Order>();


            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new Order()
                {
                    CustId = row["CustomerId"].ToString(),
                    CustName = row["CustName"].ToString(),
                    EmpId = (int)row["EmployeeID"],
                    EmpName = row["EmpName"].ToString(),
                    Freight = (decimal)row["Freight"],
                    Orderdate = row["Orderdate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["Orderdate"],
                    OrderId = (int)row["OrderId"],
                    RequireDdate = row["RequireDdate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["RequireDdate"],
                    ShipAddress = row["ShipAddress"].ToString(),
                    ShipCity = row["ShipCity"].ToString(),
                    ShipCountry = row["ShipCountry"].ToString(),
                    ShipName = row["ShipName"].ToString(),
                    ShippedDate = row["ShippedDate"] == DBNull.Value ? (DateTime?)null : (DateTime)row["ShippedDate"],
                    ShipperId = (int)row["ShipperID"],
                    ShipperName = row["ShipperName"].ToString(),
                    ShipPostalCode = row["ShipPostalCode"].ToString(),
                    ShipRegion = row["ShipRegion"].ToString()
                });
            }
            return result;
        }
    }
}