using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.UserControls
{
    public partial class Ucpages : System.Web.UI.UserControl  //先建立4個屬性(prompty)
    {
        public string Url { get; set; }   //URL

        public int TotalSize { get; set; } //總筆數

        public int PageSize { get; set; } //頁面筆數
        public int CurrentPage { get; set; } //目前頁數

        protected void Page_Load(object sender, EventArgs e)  
        {
            int totalpages = this.GetTotalPages();

            this.ltPager.Text = $"共{this.TotalSize}筆,共{totalpages}頁,目前在第{this.GetCurrentPage()}頁<br/>";

            for (var i = 1; i <= totalpages; i++)
            {
                this.ltPager.Text += $"<a href='{this.Url}?page={i}'>{i}</a>&nbsp;";  //nbsp是空格
            }


        }

        private int GetCurrentPage()   //判斷現在為第幾頁
        {
            string pageText = Request.QueryString["Page"];
            if (string.IsNullOrWhiteSpace(pageText)) //如果是空字串
                return 1;//當成第一頁回傳

            int intpage;
            if (!int.TryParse(pageText, out intpage)) //如果轉換失敗
                return 1; //也是回傳第一頁

            if (intpage <= 0)
                return 1;
            return intpage;

        }

        private int GetTotalPages()  //取得總頁數
        {
            int pagers = this.TotalSize / this.PageSize;
            if ((this.TotalSize % this.PageSize) > 0)
                pagers += 1;

            return pagers;
        }
    }
}