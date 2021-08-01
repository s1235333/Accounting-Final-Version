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

            //check empty
            if (string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            {
                this.ltlMsg.Text = "帳號或密碼為必填.";
                return; //一旦發生錯誤,後面程式不跑
            }

            var dr = UserInfoManager.GetUserInfoByAccount(inp_Account);//從DB裡面查詢使用者輸入的account

            if (dr == null) //如果是個資料不存在
            {
                this.ltlMsg.Text = "帳號不存在";
                return;
            }


            //check account/pwd
            if (string.Compare(dr["Account"].ToString(), inp_Account, true) == 0 && //帳號本身忽略大小寫
                string.Compare(dr["PWD"].ToString(), inp_PWD, false) == 0) //compare去比對輸入內容跟資料庫的資料
            {
                this.Session["UserLoginInfo"] = dr["Account"].ToString(); //帳號寫到session內,才能在別的頁面知道登入狀況
                Response.Redirect("/SystemAdmin/UserInfo.aspx"); //登入成功,就導入到用戶頁面
            }
            else
            {
                this.ltlMsg.Text = "登入失敗,請檢查帳號密碼";
                return;
            }
        }
    }
}