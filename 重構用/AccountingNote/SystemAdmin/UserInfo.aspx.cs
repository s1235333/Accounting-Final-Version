using AccountingNote.Auth;
using NewAccountungNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) //檢查使用者是否登入
        {
            if (!this.IsPostBack) //可能是按扭跳回本頁,所以要判斷PostBack
            {
                if (!AuthManager.IsLogined()) //先檢查session是存在,如果不存在(沒登入過),便導回登入頁
                {
                    Response.Redirect("/Login.aspx"); //沒登入過,便導回登入頁
                    return;
                }

                var currentUser=AuthManager.GetCnrrentUser();
               
                if (currentUser == null) //有可能帳號被管理者移除掉,帳號不存在
                {
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = currentUser.Account;
                this.ltName.Text = currentUser.Name;
                this.ltEmail.Text = currentUser.Email;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e) // 登出
        {
            AuthManager.Logout(); //呼叫AuthManager中的方法
            Response.Redirect("/Login.aspx");//跳回登入頁
        }

        protected void btnLogout_Click1(object sender, EventArgs e)
        {

        }
    }
}