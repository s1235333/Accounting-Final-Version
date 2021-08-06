using AccountingNote.Auth;
using NewAccountungNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) //讀取Accounting的資料
        {    //以下依然驗證登入
           if(!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx"); //沒登入過,便導回登入頁
                return;
            }
            //透過Session先取得現在登入者為誰,得到使用者完整資訊
            string account = this.Session["UserLoginInfo"] as string;//取得帳號
            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null) //有可能帳號被管理者移除掉,帳號不存在
            {
                this.Session["UserLoginInfo"] = null; //為了避免無限迴圈(個人資訊頁及登入頁),因此清空Session
                Response.Redirect("/Login.aspx");
                return;
            }
            //read accounting data
            var dt = AccountingManager.GetAccountingList(currentUser.ID); //取得ID,查詢完後把ID當作參數傳出

            //以下做是否為0筆資料的判斷
            if (dt.Rows.Count > 0) //資料筆數大於0
            {
                this.gvAccountingList.DataSource = dt; //資料繫結
                this.gvAccountingList.DataBind();


                //以下做金額加總
                int totalMoneyCost = 0; //宣告初始值為0
                foreach(DataRow dr in dt.Rows)
                {
                    int actType = dr.Field<int>("ActType");
                    if (actType == 1) totalMoneyCost += dr.Field<int>("Amount");
                    if (actType == 0) totalMoneyCost -= dr.Field<int>("Amount");
                }

                this.lbl_Total.Text = $"目前總金額:{totalMoneyCost}";
            }
            else  //資料筆數為0
            {
                this.gvAccountingList.Visible = false;
                this.PlcNoData.Visible = true;

            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            

        }


        //以下程式碼處理支出及收入的控制項
        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e) //資料繫結  
        {
            var row = e.Row; //每一列樣板實體化的內容

            if (row.RowType == DataControlRowType.DataRow)
            {
                //Literal ltl = row.FindControl("ltActType") as Literal;
                Label lbl = row.FindControl("lblActType") as Label;
                //ltl.Text = "OK";

                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");

                if (actType == 0)
                {
                    //ltl.Text = "支出";
                    lbl.Text = "支出";
                }

                else
                {
                    //ltl.Text = "收入";
                    lbl.Text = "收入";
                }

                if(dr.Row.Field<int>("Amount")>1500) //假設金額大於1500
                {
                    lbl.ForeColor = Color.Red;
                }
            }

        }

        protected void btnCreate_Click1(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx"); //可以導入至細節處理
        }
    }

}
