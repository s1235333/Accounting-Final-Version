using AccountingNote.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserList3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AccountingNote.Auth.AuthManager.IsLogined())//不存在(沒登入過),便導回登入頁
            {
                return;
            }
            var cUser = AccountingNote.Auth.AuthManager.GetCurrentUser(); //取得使用者為誰
           
            this.GridView1.DataSource=
            NewAccountungNote.DBSource.AccountingManager.GetAccountingList(cUser.ID);
            this.GridView1.DataBind();
        }
    }
}