using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample_5.BLL;

namespace Sample_5
{
    public partial class ChannelScanList : System.Web.UI.Page
    {
        /// <summary>
        /// 渠道扫描记录列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {                
            //获取渠道ID
            int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                //获取渠道扫描记录列表数据
                ViewState["ChannelScanDisplayList"] = new ChannelScanBll().GetChannelScanList(id);
            }
        }
    }
}