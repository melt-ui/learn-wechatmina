using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample_5.BLL;
using Sample_5.ViewEntity;

namespace Sample_5
{
    public partial class ChannelEdit : System.Web.UI.Page
    {
        /// <summary>
        /// 新增或修改渠道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //获取渠道类型列表数据
                ViewState["ChannelTypeList"] = new ChannelTypeBll().GetEntities();
                //修改渠道，首先获取渠道数据
                int id;
                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    ViewState["Channel"] = new ChannelBll().GetEntityById(id);
                }
            }
            else
            {
                //将渠道新增或修改的数据保存到数据库
                var entity = new ChannelEntity()
                {
                    ID = Request.Form["ID"] == null ? 0 : int.Parse(Request.Form["ID"]),
                    Name = Request.Form["Name"],
                    ChannelTypeId = int.Parse(Request.Form["ChannelTypeId"])
                };
                new ChannelBll().UpdateOrInsertEntity(entity);
                //回到渠道列表页面
                Response.Redirect("ChannelList.aspx");
                Response.End();
            }
        }
    }
}