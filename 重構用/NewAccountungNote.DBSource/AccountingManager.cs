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

        /// <summary>/// 查詢流水帳清單 /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetAccountingList(string userID) //拿userID當成過濾條件
        {
            string connStr = DBHelper.GetConnectionString(); //建立連線字串
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

            List<SqlParameter> list = new List<SqlParameter>(); //透過List把SqlParameter裝起來
            list.Add(new SqlParameter("@userID", userID));//建立參數 
            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);//將上面參數傳給這個method
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }


        /// <summary>/// 查詢流水帳清單 /// </summary>
        /// <param name="id"></param>
        ///  /// <param name="userID"></param>
        /// <returns></returns>
        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnectionString(); //建立連線字串
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
            List<SqlParameter> list = new List<SqlParameter>(); //透過List把SqlParameter裝起來
            list.Add(new SqlParameter("@id", id));//建立參數 
            list.Add(new SqlParameter("@userID", userID));//建立參數 

            try
            {
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }

        }



        /// <summary> 新增建立流水帳/// </summary>
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
            if (actType < 0 || actType > 1)
            {
                throw new ArgumentException("ActType must between 0 or 1.");
            }

            string connStr = DBHelper.GetConnectionString();
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
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now); //直接取當下時間即可
                    comm.Parameters.AddWithValue("@body", body);
                    try
                    {
                        conn.Open(); //連線開啟
                        comm.ExecuteNonQuery();
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

            string connStr = DBHelper.GetConnectionString();
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
                        ID = @id  ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID)); //參數化查詢
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now)); //直接取當下時間即可
            paramList.Add(new SqlParameter("@body", body));
            paramList.Add(new SqlParameter("@id", ID));

            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);
                if (effectRows == 1) //更動筆數
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



        /// <summary> 刪除流水帳/// </summary>
        /// <param name="ID"></param>

        public static void DeleteAccounting(int ID)
        {
            string connstr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"DELETE [Accounting]
                 WHERE ID = @id";

            List<SqlParameter> ParamList = new List<SqlParameter>(); //參數往外挪
            ParamList.Add(new SqlParameter("@id", ID));


            try
            {
                DBHelper.ModifyData(connstr, dbCommand, ParamList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }

        }
    }
}







