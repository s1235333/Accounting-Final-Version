using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable StartAct = NewAccountungNote.DBSource.UserInfoManager.GetDataBase("AS");
            DataTable Userinfo = NewAccountungNote.DBSource.UserInfoManager.GetDataBase("A");

            if (Userinfo == null)
            {
                this.ltlmsg.Text = "尚未有使用者";
                return;
            }

            if (StartAct == null)
            {
                this.ltlmsg.Text = $"第一筆紀錄日期:尚未輸入第一筆紀錄<br/>";

            }
            int LastRowsint = StartAct.Rows.Count - 1;
            DataRow FirstRow = StartAct.Rows[0];     //最早記帳時間
            DataRow LastRow = StartAct.Rows[LastRowsint]; //最後記帳時間


            

            //以下輸出
            this.ltlFirst.Text = $"{FirstRow["CreateDate"].ToString()}";
            this.ltlLast.Text = $"{LastRow["CreateDate"].ToString()}";
            this.ltlCount.Text = $"共{StartAct.Rows.Count}筆";
            this.ltlMember.Text = $"共{Userinfo.Rows.Count}筆";



        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserInfo.aspx");
        }
    }
}

