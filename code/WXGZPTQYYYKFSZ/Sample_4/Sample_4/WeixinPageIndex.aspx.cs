using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;

namespace Sample_4
{
    public partial class WeixinPageIndex : System.Web.UI.Page
    {
        /// <summary>
        /// 从微信公众平台获取的开发者凭据
        /// </summary>
        const string appID = "wx637267c8ab5abf7f";
        /// <summary>
        /// 从微信公众平台获取的开发者凭据
        /// </summary>
        const string appsecret = "0703c5e946a658409b48386e45295e4a";

        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection parameters = System.Web.HttpContext.Current.Request.Params;
            //取得链接中的分享者OpenId
            string shareOpenId = parameters["s"];
            //获取访问者的OpenId
            string navOpenId = GetNavOpenId();
            if (navOpenId != null)
            {
                NavStatistics(navOpenId, shareOpenId);
                //传递给页面的访问者OpenId
                ViewState["navOpenId"] = navOpenId;
                //传递给页面的分享者OpenId
                ViewState["shareOpenId"] = shareOpenId;
            }
        }

        /// <summary>
        /// 记录页面访问
        /// </summary>
        /// <param name="navOpenId">访问者微信openid</param>
        /// <param name="shareOpenId">当访问来源为朋友圈时的分享者微信openid</param>
        private void NavStatistics(string navOpenId, string shareOpenId)
        {
            //获取访问来源
            NavFrom fromType = GetNavFromType();
            //构造访问记录
            var pageNav = new PageNavEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Url = GetOrigenalUrl(),
                NavOpenId = navOpenId,
                ShareOpenId = shareOpenId,
                From = fromType,
                VisitTime = DateTime.Now
            };
            //访问记录写入数据库
            StatisticsBll.InsertPageNav(pageNav);
        }

        /// <summary>
        /// 判断页面访问来源类型
        /// </summary>
        /// <returns></returns>
        private static NavFrom GetNavFromType()
        {
            //网址中的参数集合
            NameValueCollection parameters = System.Web.HttpContext.Current.Request.Params;
            string fromStr = parameters["from"]; //发送给朋友、分享到朋友圈的链接会含有from参数
            NavFrom fromType;
            if (!Enum.TryParse<NavFrom>(fromStr, true, out fromType)) //通过判断from参数，识别页面访问是来自于发送给朋友的链接还是分享到朋友圈的链接
            {
                //获取HTTP访问头中的User-Agent参数的值
                string agent = System.Web.HttpContext.Current.Request.Headers["User-Agent"];
                if (agent.Contains(NavFrom.MicroMessenger.ToString())) //判断页面是否是在微信内置浏览器中打开
                    fromType = NavFrom.MicroMessenger; 
                else
                    fromType = NavFrom.Other;
            }
            return fromType;
        }

        /// <summary>
        /// 获取不含统计相关参数的页面地址
        /// </summary>
        /// <returns>不含统计相关参数的页面地址</returns>
        private string GetOrigenalUrl()
        {
            StringBuilder urlBuilder = new StringBuilder();
            //获取不含QueryString的URL
            urlBuilder.Append("http://")
                .Append(System.Web.HttpContext.Current.Request.Url.Host)
                .Append(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
                .Append("?");
            //构造移除统计相关参数的Query
            foreach (var key in System.Web.HttpContext.Current.Request.QueryString.AllKeys)
            {
                if (key != "s" && key != "u" && key != "from" && key != "code" && key != "state")
                {
                    urlBuilder.Append(key).Append("=").Append(System.Web.HttpContext.Current.Request.QueryString[key]).Append("&");
                }
            }
            return urlBuilder.ToString();
        }

        /// <summary>
        /// 获取访问者openId
        /// </summary>
        private string GetNavOpenId()
        {
            NameValueCollection parameters = System.Web.HttpContext.Current.Request.Params;
            //获取链接中的openId
            string navOpenId = parameters["u"];
            #region 如果是从微信浏览器浏览，获取真实的微信OpenId
            if (!string.IsNullOrEmpty(appID) && !string.IsNullOrEmpty(appsecret))
            {
                string accessSource = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
                if (accessSource.Contains("MicroMessenger")) //如果是从微信打开页面
                {
                    string[] cookieKeys = new[] { CookieHelper.COOKIE_NAME };
                    Dictionary<string, string> realIdCookie = CookieHelper.GetLoginCookies(cookieKeys); //获取保存在Cookie中的OpenId
                    //如果Cookie中不存在OpenId，或者链接中的openId与Cookie中的OpenId不一致，链接中的openId为分享者的OpenId，需要获取当前用户的真实OpenId
                    if (NeedGetReadOpenId(parameters, realIdCookie)) 
                    {
                        if (parameters["code"] == null)
                        {
                            // 先去获取code,并记录分享者
                            string snsapi_baseUrl = GoCodeUrl(navOpenId);
                            if (!string.IsNullOrEmpty(snsapi_baseUrl))
                            {
                                CookieHelper.CleanLoginCookie(cookieKeys);
                                //跳转到微信网页授权页面
                                System.Web.HttpContext.Current.Response.Redirect(snsapi_baseUrl, true);
                                System.Web.HttpContext.Current.Response.End();
                                return null;
                            }
                        }
                        else
                        {
                            OAuthAccessTokenResult tokenResult = GetRealOpenId(parameters["code"].ToString());
                            if (null != tokenResult && !string.IsNullOrEmpty(tokenResult.openid))
                            {
                                navOpenId = tokenResult.openid;
                                // 获取到的当前访问者的OpenId保存到cookie里
                                CookieHelper.CleanLoginCookie(cookieKeys);
                                realIdCookie[CookieHelper.COOKIE_NAME] = tokenResult.openid;
                                CookieHelper.WriteLoginCookies(realIdCookie, DateTime.MinValue);
                            }
                        }
                    }
                }
            }
            #endregion
            return navOpenId;
        }

        /// <summary>
        /// 如果Cookie中存在OpenId且链接中的openId与Cookie中的OpenId一致
        /// 则不需要调用网页授权接口，链接中的openId即为当前访问者的真实OpenId
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="realIdCookie"></param>
        /// <returns></returns>
        private bool NeedGetReadOpenId(NameValueCollection parameters, Dictionary<string, string> realIdCookie)
        {
            string referer = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            string openId = null;
            if (realIdCookie != null)
            {
                if (realIdCookie.ContainsKey(CookieHelper.COOKIE_NAME))
                {
                    openId = realIdCookie[CookieHelper.COOKIE_NAME];
                }
            }
            if (!string.IsNullOrEmpty(referer) && openId == parameters["u"].ToString())
                return false;
            else
                return true;
        }

        /// <summary>
        /// 网页授权接口第一步
        /// 跳转到获取code的url
        /// </summary>
        /// <param name="shareOpenId">当访问来源为朋友圈时的分享者微信openid</param>
        private string GoCodeUrl(string shareOpenId)
        {
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri + "&s=" + shareOpenId; //添加分享者OpenId
            return OAuth.GetAuthorizeUrl(appID, url, "STATE", OAuthScope.snsapi_base);
        }

        /// <summary>
        /// 网页授权接口第二步
        /// 解析code并获取当前访问者真正的openId
        /// </summary>
        /// <param name="parameters">url参数</param>
        /// <returns>真正的openId</returns>
        private OAuthAccessTokenResult GetRealOpenId(string code)
        {
            OAuthAccessTokenResult result = new OAuthAccessTokenResult();
            try
            {
                result = OAuth.GetAccessToken(appID, appsecret, code);
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}