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

        public static bool TryLogin(string inp_Account, string inp_PWD, out object msg)
        {
            throw new NotImplementedException();
        }

        /// <summary> 登出/// </summary>

        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null; //清除登入資訊
        }


        /// <summary>
        /// 嘗試登入
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static bool TryLogin(string account,string pwd,out 
            string errorMsg)
        {

            //check empty
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg= "帳號或密碼為必填.";
                return false; //一旦發生錯誤,後面程式不跑
            }


            //讀取資料庫並檢查
            var dr = UserInfoManager.GetUserInfoByAccount(account);//從DB裡面查詢使用者輸入的account

            if (dr == null) //如果是個資料不存在
            {
                errorMsg = $"Account:{account}帳號不存在";
                return false;
            }

            //check account/pwd
            if (string.Compare(dr["Account"].ToString(), account, true) == 0 && //帳號本身忽略大小寫
                string.Compare(dr["PWD"].ToString(), pwd, false) == 0) //compare去比對輸入內容跟資料庫的資料
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString(); //帳號寫到session內,才能在別的頁面知道登入狀況
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "登入失敗,請檢查帳號密碼";
                return false;
            }


        }
    }
}
