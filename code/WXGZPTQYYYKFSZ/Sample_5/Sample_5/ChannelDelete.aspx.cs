using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample_5.BLL;

namespace Sample_5
{
    public partial class ChannelDelete : System.Web.UI.Page
    {
        /// <summary>
        /// 删除渠道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取渠道ID
            int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                //删除渠道并返回删除结果
                bool result = new ChannelBll().DeleteEntityById(id);
                Response.Write(result.ToString());
                Response.End();
            }
        }
    }
}