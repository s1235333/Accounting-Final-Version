using AccountingNote.Auth;
using NewAccountungNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] != null) //session的值是否為空值,不是空值=已登入過
            {
                this.plcLogin.Visible = false; //則關閉登入控制項們
                Response.Redirect("/SystemAdmin/UserInfo.aspx");//並導入使用者頁面
            }
            else //未登入過
            {
                this.plcLogin.Visible = true; //登入控制項開啟
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string inp_Account = this.txtAccount.Text;//取得使用者input的資訊
            string inp_PWD = this.txtPWD.Text;   //確定使用者的是否正確

            string msg;
            if (!AuthManager.TryLogin(inp_Account, inp_PWD, out msg)) //假設tryLogin是失敗的
            {
                this.ltlMsg.Text = msg;
                return;
            }

            Response.Redirect("/SystemAdmin/UserInfo.aspx");
        }
        
    }
}