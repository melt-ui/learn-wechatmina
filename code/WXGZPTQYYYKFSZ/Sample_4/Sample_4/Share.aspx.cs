using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample_4
{
    /// <summary>
    /// 接收分享记录信息并保存到数据库
    /// </summary>
    public partial class Share : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeStr = Request.QueryString["type"];
            if (!string.IsNullOrEmpty(typeStr))
            {
                //识别分享类型
                ShareType type = ShareType.Unknown;
                switch (typeStr)
                {
                    case "timeline":
                        type = ShareType.Timeline;
                        break;
                    case "friend":
                        type = ShareType.Friend;
                        break;
                }
                //构造分享记录
                var pageShare = new PageShareEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Url = GetOrigenalUrl(System.Web.HttpContext.Current.Request.QueryString["url"]),
                    ParentShareOpenId = System.Web.HttpContext.Current.Request.QueryString["s"],
                    ShareOpenId = System.Web.HttpContext.Current.Request.QueryString["u"],
                    From = type,
                    ShareTime = DateTime.Now
                };
                //保存分享记录
                StatisticsBll.InsertPageShare(pageShare);
            }
        }

        /// <summary>
        /// 获取不含统计相关参数的页面地址
        /// </summary>
        /// <param name="url">网址</param>
        /// <returns>不含统计相关参数的页面地址</returns>
        private string GetOrigenalUrl(string url)
        {
            url = System.Web.HttpUtility.UrlDecode(url);
            Uri uri = new Uri(url);
            StringBuilder urlBuilder = new StringBuilder();
            //获取不含QueryString的URL
            urlBuilder.Append("http://")
                .Append(uri.Host)
                .Append(uri.AbsolutePath)
                .Append("?");
            //构造移除统计相关参数的Query
            Dictionary<string, string> queryString = uri.Query.Replace("?", "").Split('&').Where(p => !string.IsNullOrEmpty(p)).ToDictionary(p => p.Split('=')[0], p => p.Split('=')[1].Split('#')[0]);
            foreach (var key in queryString.Keys)
            {
                if (key != "s" && key != "u" && key != "from" && key != "code" && key != "state")
                {
                    urlBuilder.Append(key).Append("=").Append(queryString[key]).Append("&");
                }
            }
            return urlBuilder.ToString();
        }
    }
}