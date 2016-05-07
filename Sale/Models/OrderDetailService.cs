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
                String sql = @" Insert INTO Sales.OrdersDetails
						 (
							OrderID,ProudctID,UnitPrice,Discount
						 )
						VALUES
						(
							@OrderID,@ProudctID,@UnitPrice,@Discount
						)
						Select SCOPE_IDENTITY()
						";
                int result;
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@OrderId", OrderId));
                    cmd.Parameters.Add(new SqlParameter("@ProudctID", row.ProductId));
                    cmd.Parameters.Add(new SqlParameter("@UnitPrice", row.UnitPrice));
                    cmd.Parameters.Add(new SqlParameter("@Discount", row.Discount));

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                }
            }    
            return 0;
        }
    }
}