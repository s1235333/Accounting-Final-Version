using AccountingNote.Auth;
using NewAccountungNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //以下依然驗證登入
            if (this.Session["UserLoginInfo"] == null) //先檢查session是存在,如果不存在(沒登入過),便導回登入頁
            {
                Response.Redirect("/Login.aspx"); //沒登入過,便導回登入頁
                return;
            }
            //透過Session先取得現在登入者為誰,得到使用者完整資訊
            string account = this.Session["UserLoginInfo"] as string;//取得帳號
            var currentUser = AuthManager.GetCnrrentUser();

            if (currentUser == null) //有可能帳號被管理者移除掉,帳號不存在
            {
                this.Session["UserLoginInfo"] = null; //為了避免無限迴圈(個人資訊頁及登入頁),因此清空Session
                Response.Redirect("/Login.aspx");
                return;
            }
            if (!this.IsPostBack)  //不是postback才來做讀取作業
            {
                //先確認是新增模式還是編輯模式(透過QueryString)
                if (this.Request.QueryString["ID"] == null) //ID是null,表示是新增模式
                {
                    this.btnDelete.Visible = false; //如果是新增模式,就不會有刪除鍵
                }
                else//不是新增模式,而是編輯修改模式,就會顯示刪除鍵
                {
                    this.btnDelete.Visible = true; 

                    string idText = this.Request.QueryString["ID"]; //取得參數id,將id存取為字串
                    int id;
                    if (int.TryParse(idText, out id)) //以下試試看將ID使用TryParse轉型成數字,並成功轉型
                    {
                        //從資料庫查出內容,並存在dr變數內
                        var drAccounting = AccountingManager.GetAccounting(id,currentUser.ID);  //簡單的二次確認是否為同一使用者
                        if (drAccounting == null)  //如果查不到資料則提醒
                        {
                            this.ltMsg.Text = "Data doesn't exist";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else //如果查得到資料
                        {
                                this.ddlActType.SelectedValue = drAccounting["ActType"].ToString();
                                this.txtAmount.Text = drAccounting["Amount"].ToString();
                                this.txtCaption.Text = drAccounting["Caption"].ToString();
                                this.txtDesc.Text = drAccounting["Body"].ToString();
    
                        }
                    }
                    else //沒有成功轉型成數字的情況
                    {
                        this.ltMsg.Text = "ID is required";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)  //Checkinput方法另外抽在下面
        {
            List<string> msgList = new List<string>(); //儲存錯誤訊息
                
            if(!this.CheckInput(out msgList)) //檢查輸入值有誤
            {
                this.ltMsg.Text = string.Join("<bt/>", msgList); //字串結合
                return;

            }
            //透過Session先取得現在登入者為誰,得到使用者完整資訊
            string account = this.Session["UserLoginInfo"] as string;//取得帳號
            var dr = UserInfoManager.GetUserInfoByAccount(account);  //取得完整使用者資料

            if (dr == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            //檢查輸入值沒問題,一個一個取得輸入值
            string userID = dr["ID"].ToString();//取出ID,並存取在userID       
            string actTypeText = this.ddlActType.SelectedValue;
            string amountText = this.txtAmount.Text;
            string caption = this.txtCaption.Text;
            string body = this.txtDesc.Text;

            //轉型
            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);

           

            string idText = this.Request.QueryString["ID"]; //取得參數id,將id存取為字串
            if(string.IsNullOrWhiteSpace(idText))  //如果取得到id這個欄位
            {
                //呼叫 Execute'Insert into db'
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body); //假設是空字串,就呼叫新增模式
            }
            else  //假設不是空字串
            {
                int id;
                if (int.TryParse(idText, out id)) //以下試試看將ID使用TryParse轉型成數字,並成功轉型
                {
                    //呼叫 Execute'update into db'
                    AccountingManager.UpdateAccounting(id,userID, caption, amount, actType, body); //假設不是空字串,就呼叫編輯模式
                }
            }
            

                Response.Redirect("/SystemAdmin/AccountingList.aspx"); //新增完一筆就導入流水帳列表頁

        }
        private bool CheckInput(out List<string>errorMsgList) //檢查輸入值是否正確 (抽方法在此)
        {
            List<string> msgList = new List<string>(); //宣告提示文字msgList  //不懂為何是用<>

            //Type
            if(this.ddlActType.SelectedValue!="0"&& this.ddlActType.SelectedValue!="1") //假設不是in也不是out,意思是漏選
            {
                msgList.Add("Type must be 0 or 1.");
            }

            //Amount
            if(string.IsNullOrWhiteSpace(this.txtAmount.Text)) //檢查金額是否為空字串
            {
                msgList.Add("Amount is required");
            }
            else  //如果不是,就先試著轉成整數
            {
                int tempInt;
                if(!int.TryParse(this.txtAmount.Text,out tempInt))  //轉換失敗,會出現提示文字   //不懂out怎麼使用
                {
                    msgList.Add("Amount must be anumber.");
                }

                if(tempInt <0 || tempInt >1000000) //流水帳值的限制
                {
                    msgList.Add("Amount must between 0 amd 1,000,000. ");
                }
            }

            errorMsgList = msgList;

            if (msgList.Count == 0) //沒有錯誤
                return true;
            else
                return false;
                 
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click1(object sender, EventArgs e)
        {

        }
    }
}