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
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null) // 如果帳號不存在，導至登入頁
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            DataTable dt = UserInfoManager.GetDataBase("U");
            if (dt.Rows.Count > 0)  // check is empty data
            {
                this.gv_UserList.DataSource = dt;
                this.gv_UserList.DataBind();
            }
            else
            {
                this.gv_UserList.Visible = false;

            }
            this.gv_UserList.DataSource = dt;
            this.gv_UserList.DataBind();


        }


        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;
            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;
            if (intPage <= 0)
                return 1;
            return intPage;
        }

        protected void btn_addUser_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/SystemAdmin/UserDetail.aspx");

            Response.Redirect("/SystemAdmin/UserDetail.aspx");

        }


    }
}