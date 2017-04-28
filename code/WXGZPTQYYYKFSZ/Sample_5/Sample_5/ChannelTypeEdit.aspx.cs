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
    public partial class ChannelTypeEdit : System.Web.UI.Page
    {
        /// <summary>
        /// 新增或修改渠道类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //修改渠道类型，首先获取渠道类型数据
                int id;
                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    ViewState["channelType"] = new ChannelTypeBll().GetEntityById(id);
                }
            }
            else
            {
                //将渠道类型新增或修改的数据保存到数据库
                var entity = new ChannelTypeEntity()
                {
                    ID = Request.Form["ID"] == null ? 0 : int.Parse(Request.Form["ID"]),
                    Name = Request.Form["Name"]
                };
                new ChannelTypeBll().UpdateOrInsertEntity(entity);
                //回到渠道类型列表页面
                Response.Redirect("ChannelTypeList.aspx");
                Response.End();
            }
        }
    }
}