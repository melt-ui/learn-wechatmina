using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample_5.BLL;

namespace Sample_5
{
    public partial class ChannelList : System.Web.UI.Page
    {
        /// <summary>
        /// 渠道列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取渠道列表数据
            ViewState["ChannelList"] = new ChannelBll().GetEntities();
        }
    }
}