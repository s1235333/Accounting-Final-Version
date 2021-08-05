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

    public class UserInfoManager  //負責和資料庫取所有跟帳號有關的資料 引用兩個取得連線字串的方法
    {


        public static DataRow GetUserInfoByAccount(string account)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                @"SELECT [ID], [Name],[PWD], [Account],[Email]
                  FROM UserInfo
                  WHERE account = @account
                    ";
            List<SqlParameter> list = new List<SqlParameter>(); //透過List把SqlParameter裝起來
            list.Add(new SqlParameter("@account", account));//建立參數 

            try
            {
                return DBHelper.ReadDataRow(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }


        //取得程式庫

        public static DataTable GetDataBase(string str)
        {
            string connstr = DBHelper.GetConnectionString();
            string dbCommand;
            if (str.CompareTo("AS") == 0)
            {
                dbCommand =
                    $@" SELECT
                        Createdate
                      FROM Accounting
                      ORDER BY CreateDate ASC;";

            }
            else if (str.CompareTo("AL") == 0)
            {
                dbCommand =
                      $@" SELECT
                        Createdate
                      FROM Accounting
                      ORDER BY CreateDate DESC;";
            }
            else
            {
                dbCommand =
                     $@" SELECT * FROM USERInfo;";
            }
            List<SqlParameter> list = new List<SqlParameter>();

            try
            {
                return DBHelper.ReadDataTable(connstr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

    }
}

