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
    public class AccountingManager
    {
        //此頁程式碼為將值從AccountingList取出
        public static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
        /// <summary>/// 查詢流水帳清單 /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetAccountingList(string userID) //拿userID當成過濾條件
        {
            string connStr = GetConnectionString(); //建立連線字串
            string dbCommand =  //以下為查詢指令,User ID 及 Body不呈現
                $@" SELECT 
                    ID,
                    Caption,
                    Amount,
                    ActType,
                    CreateDate
                 FROM Accounting
                 WHERE userID=@userID
                ";

            using (SqlConnection conn = new SqlConnection(connStr))  //連線用物件
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))//下命令用的物件
                {
                    comm.Parameters.AddWithValue("@userID", userID); //參數化查詢userID
                    try
                    {
                        conn.Open(); //連線開啟
                        var reader = comm.ExecuteReader(); //command執行Reader後把值取回,存入reader

                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>/// 查詢流水帳清單 /// </summary>
        /// <param name="id"></param>
        ///  /// <param name="userID"></param>
        /// <returns></returns>
        public static DataRow GetAccounting(int id ,string userID)
        {
            string connStr = GetConnectionString(); //建立連線字串
            string dbCommand =  //以下為查詢指令,同時使用id及user ID就可以避免看到他人資料
                $@" SELECT 
                    ID,
                    Caption,
                    Amount,
                    ActType,
                    CreateDate,
                    Body
                 FROM Accounting
                 WHERE id =@id AND UserID =@userID
                ";

            using (SqlConnection conn = new SqlConnection(connStr))  //連線用物件
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))//下命令用的物件
                {
                    comm.Parameters.AddWithValue("@id", id); //參數化查詢userID
                    comm.Parameters.AddWithValue("@userID", userID); //參數化查詢userID
                    try
                    {
                        conn.Open(); //連線開啟
                        var reader = comm.ExecuteReader(); //command執行Reader後把值取回,存入reader

                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        if (dt.Rows.Count == 0) //回傳編輯的單筆即可
                            return null;
                        else
                            return dt.Rows[0];
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }

        } 

        /// <summary> 建立新增流水帳/// </summary>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body) 
        {
            //先確認輸入值合不合理(amount.ActType)
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000.");
            }
            if(actType < 0 || actType >1)
            {
                throw new ArgumentException("ActType must between 0 or 1.");
            }

            string connStr = GetConnectionString();
            string dbCommand =
                $@" INSERT INTO [dbo].[Accounting]
                     (
                        UserID
                       ,Caption
                       ,Amount
                       ,ActType
                       ,CreateDate
                       ,Body
                     )
                     VALUES
                      (
                        @userID
                       ,@caption
                       ,@amount
                       ,@actType
                       ,@createDate
                       ,@body
                       )
                 ";

            //連線db及命令
            using (SqlConnection conn = new SqlConnection(connStr))  //連線用物件
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))//下命令用的物件
                {
                    comm.Parameters.AddWithValue("@userID", userID); //參數化查詢
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate",DateTime.Now); //直接取當下時間即可
                    comm.Parameters.AddWithValue("@body", body);
                    try
                    {
                        conn.Open(); //連線開啟
                        comm.ExecuteNonQuery(); //

                       
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        
                    }
                }
            }

        }

        /// <summary> 更新編輯流水帳/// </summary>
        /// <param name="ID"></param>
        /// <param name="userID"></param>
        /// <param name="caption"></param>
        /// <param name="amount"></param>
        /// <param name="actType"></param>
        /// <param name="body"></param>  //拿id當作過濾條件
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            //先確認輸入值合不合理(amount.ActType)
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000.");
            }
            if (actType < 0 || actType > 1)
            {
                throw new ArgumentException("ActType must between 0 or 1.");
            }

            string connStr = GetConnectionString();
            string dbCommand =
                $@" UPDATE [Accounting]     
                       SET
                        UserID      = @userID
                       ,Caption     = @caption
                       ,Amount      =@amount
                       ,ActType     =actType
                       ,CreateDate  =@createDate
                       ,Body        =@body
                  WHERE
                        ID = @id
                        
                 ";

            //連線db及命令
            using (SqlConnection conn = new SqlConnection(connStr))  //連線用物件
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))//下命令用的物件
                {
                    comm.Parameters.AddWithValue("@userID", userID); //參數化查詢
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now); //直接取當下時間即可
                    comm.Parameters.AddWithValue("@body", body);
                    comm.Parameters.AddWithValue("@id", ID);
                    try
                    {
                        conn.Open(); //連線開啟
                        int effectRows=comm.ExecuteNonQuery(); //受影響的資料

                        if (effectRows == 1 ) //更動筆數
                            return true;
                        else
                            return false; //沒有成功更新


                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return false;

                    }
                }
            }

        }


    }
}
