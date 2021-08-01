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
                if (this.Session["UserLoginInfo"] == null) //先檢查session是存在,如果不存在(沒登入過),便導回登入頁
                {
                    Response.Redirect("/Login.aspx"); //沒登入過,便導回登入頁
                    return;
                }
                string account = this.Session["UserLoginInfo"] as string; //account為UserLoginInfo所取得值,轉成字串

                DataRow dr = UserInfoManager.GetUserInfoByAccount(account); //查詢使用者資料是否真的存在

                if (dr == null) //有可能帳號被管理者移除掉,帳號不存在
                {
                    this.Session["UserLoginInfo"] = null; //為了避免無限迴圈(個人資訊頁及登入頁),因此清空Session
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = dr["Account"].ToString();
                this.ltName.Text = dr["Name"].ToString();
                this.ltEmail.Text = dr["Email"].ToString();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e) // 登出
        {
            this.Session["UserLoginInfo"] = null; //清除登入資訊
            Response.Redirect("/Login.aspx");//跳回登入頁
        }
    }
}