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
            public static string GetConnectionString()
            {
                string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                return val;
            }

            public static DataRow GetUserInfoByAccount(string account)
            {
                string connectionString = GetConnectionString();
                string dbCommandString =
                    @"SELECT [ID], [Name],[PWD], [Account],[Email]
                  FROM UserInfo
                  WHERE account = @account
                    ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                    {
                        command.Parameters.AddWithValue("@account", account); //參數化查詢

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            reader.Close();

                            if (dt.Rows.Count == 0)
                                return null;

                            DataRow dr = dt.Rows[0];
                            return dr;
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLog(ex); //這裡有開一個類別
                            return null;
                        }
                    }
                }
            }


        }
    }

