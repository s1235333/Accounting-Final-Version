using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.IsLogined()) //先檢查session是存在,如果不存在(沒登入過),便導回登入頁
            {
                Response.Redirect("/Login.aspx"); //沒登入過,便導回登入頁
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null) //有可能帳號被管理者移除掉,帳號不存在
            {
                Response.Redirect("/Login.aspx");
                return;
            }

        }
    }
}