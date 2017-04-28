using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample_5.BLL;

namespace Sample_5
{
    public partial class QRcode : System.Web.UI.Page
    {
        /// <summary>
        /// 下载渠道二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取渠道ID
            int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                var entity = new ChannelBll().GetEntityById(id);
                //将数据库中Base64String格式图片转换为Image格式，返回给浏览器
                string base64Image = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/") + entity.Qrcode);
                byte[] arr = Convert.FromBase64String(base64Image);
                MemoryStream ms = new MemoryStream(arr);
                Response.ContentType = "image/jpeg";
                ms.WriteTo(Response.OutputStream);
                Response.End();
            }
        }
    }
}