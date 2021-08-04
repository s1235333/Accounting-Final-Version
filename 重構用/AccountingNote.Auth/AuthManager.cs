using NewAccountungNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    /// <summary> 負責處理登入的元件 /// </summary>

    public class AuthManager
    {
        /// <summary>檢查目前是否登入/// </summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session
             ["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 取得已登入的使用者資訊(如果沒有登入就回傳null)
        /// </summary>
        /// <returns></returns>
        public static UserInfoModel GetCnrrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)//沒有人登入
                return null;

            //透過登入取得個人資訊
            DataRow dr = UserInfoManager.GetUserInfoByAccount
            (account);

            if (dr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }
            //return dr;

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.PWD = dr["PWD"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;
        }

        /// <summary> 清除登/// </summary>

        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null; //清除登入資訊
        }
    }
}
