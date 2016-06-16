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
    public class OrderDetailService
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
        /// 新增訂單明細
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int InsertOrderDetail(List<Models.OrderDetails> orderDetail, int OrderId)
        {
            foreach(Models.OrderDetails row in orderDetail)
            {
                String sql = @" Insert INTO Sales.OrderDetails
						 (
							OrderID,ProductID,Qty,UnitPrice,Discount
						 )
						VALUES
						(
							@OrderId,@ProductID,@Qty,@UnitPrice,@Discount
						)
						Select SCOPE_IDENTITY()
						";
                
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@OrderId", OrderId));
                    cmd.Parameters.Add(new SqlParameter("@ProductID", row.ProductId));
                    cmd.Parameters.Add(new SqlParameter("@UnitPrice", row.UnitPrice));
                    cmd.Parameters.Add(new SqlParameter("@Qty", row.Qty));
                    cmd.Parameters.Add(new SqlParameter("@Discount", row.Discount));

                    //result = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }    
            return 0;
        }

        public List<Models.OrderDetails> GetOrderByOrderId(string OrderId)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT OrderID, OD.ProductID, OD.UnitPrice, Qty, Discount, ProductName
                        FROM Sales.OrderDetails AS OD
                        INNER JOIN Production.Products AS P ON OD.ProductID = P.ProductID
                        WHERE OrderID=@OrderId";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", OrderId));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapOrderDetailsDataToList(dt);
        }

        private List<Models.OrderDetails> MapOrderDetailsDataToList(DataTable orderData)
        {
            List<Models.OrderDetails> result = new List<OrderDetails>();

            foreach (DataRow row in orderData.Rows)
            {
                result.Add(new OrderDetails()
                {
                    OrderId = (int)row["OrderID"],
                    ProductName = (string)row["ProductName"],
                    ProductId = (int)row["ProductID"],
                    UnitPrice = (decimal)row["UnitPrice"],
                    Qty = (short)row["Qty"],
                    Discount = (decimal)row["Discount"]
                });
            }
            return result;
        }
    }
}