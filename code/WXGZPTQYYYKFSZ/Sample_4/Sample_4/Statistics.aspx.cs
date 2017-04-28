using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample_4
{
    public partial class Statistics1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //传递给页面显示的记录列表
            ViewState["NavList"] = StatisticsBll.GetPageNavList();
            ViewState["ShareList"] = StatisticsBll.GetPageShareList();
        }
    }
}