using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAccountungNote.DBSource
{
    public class DBHelper
    {
        //連線字串
        public static string GetConnectionString() 
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataTable ReadDataTable(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection conn = new SqlConnection(connStr))  //連線用物件
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))//下命令用的物件
                {
                    //comm.Parameters.AddWithValue("@userID", userID); //參數化查詢userID
                    comm.Parameters.AddRange(list.ToArray()); //AddRange 是一次加很多筆的意思,使用ToArray 把清單轉成陣列

                    conn.Open(); //連線開啟
                    var reader = comm.ExecuteReader(); //command執行Reader後把值取回,存入reader

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    return dt;
                }
            }
        }
    }
}
