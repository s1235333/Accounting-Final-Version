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
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = this.Session["UserLoginInfo"] as string;
            string newbeein = this.Session["FromUserDetail"] as string;
            var currentUser = AuthManager.GetCurrentUser();

            if (newbeein != null && newbeein.CompareTo("yesfromdetail") == 0)  //若使用者是第一位，則破例讓他創
            {

            }
            else if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            else if (currentUser == null || account == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["ID"] == null)
                {
                    this.btnDelete.Visible = false;
                    //若要新增使用者，那就要讓他們打密碼，更新無法打密碼
                    this.PlaceHolderPWD.Visible = true;
                    this.pwLink.Visible = false;
                }
                else
                {
                    this.btnDelete.Visible = true;

                    string idText = this.Request.QueryString["ID"];
                    if (!string.IsNullOrWhiteSpace(idText))
                    {
                        var drAccounting = UserInfoManager.GetUserInfoByUID(idText);

                        if (drAccounting == null)
                        {
                            this.ltMsg.Text = "資料不存在";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
                            string pwdcText = this.Request.QueryString["Txt"];
                            if (!string.IsNullOrWhiteSpace(pwdcText)) this.ltMsg.Text = "密碼變更完成！";
                            this.txtAccount.Text = drAccounting["Account"].ToString();
                            this.txtName.Text = drAccounting["Name"].ToString();
                            this.txtMail.Text = drAccounting["Email"].ToString();
                        }
                    }
                    else
                    {
                        this.ltMsg.Text = "需要ID";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }



                }
            }
        }


    

    protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }


            UserInfoModel currentUser = AuthManager.GetCurrentUser();
            string isnew = this.Session["FromUserDetail"] as string;
            if (isnew != null && isnew.CompareTo("yesfromdetail") == 0)  //若使用者是第一位，則破例讓他創
            {
                //this.Session["UserLoginInfo"] = null;
            }
            else if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            //string userID = currentUser.ID;
            string account = this.txtAccount.Text;
            string name = this.txtName.Text;
            string email = this.txtMail.Text;


            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText))
            {
                string pwd = this.txtPassword.Text;
                UserInfoManager.CreateUser(account, pwd, name, email);
                //AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else
            {
                UserInfoManager.UpdateUser(idText, account, name, email);
            }

            Response.Redirect("/SystemAdmin/UserList.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.txtAccount.Text)) msgList.Add("帳號沒打");
            if (string.IsNullOrWhiteSpace(this.txtName.Text)) msgList.Add("稱呼沒打");
            if (string.IsNullOrWhiteSpace(this.txtMail.Text)) msgList.Add("信箱沒打");
            if (this.txtPassword.Visible)
            {
                if (string.IsNullOrWhiteSpace(this.txtPassword.Text)) msgList.Add("密碼沒打");
                else if (string.IsNullOrWhiteSpace(this.txtPWDCheck.Text)) msgList.Add("確認密碼欄位沒打");
                else if (this.txtPassword.Text.CompareTo(this.txtPWDCheck.Text) != 0) msgList.Add("兩次輸入的密碼不相同");
                else if (this.txtPassword.Text.Length < 8 || this.txtPassword.Text.Length > 16) msgList.Add("密碼必須在8~16碼之間");
            }

            errorMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];

            if (string.IsNullOrWhiteSpace(idText))
                return;

            UserInfoManager.DeleteAccounting(idText);

            Response.Redirect("/SystemAdmin/UserList.aspx");

        }
    }
}