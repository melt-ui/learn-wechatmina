using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public static class User
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public static UserInfoJson Info(string accessToken, string openId, Language lang = Language.zh_CN)
        {
            //微信公众平台获取用户信息接口地址
            string urlFormat = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}";
            return ApiHelper.Get<UserInfoJson>(accessToken, urlFormat, openId, lang.ToString());
            //错误时微信会返回错误码等信息，JSON数据包示例如下（该示例为AppID无效错误）:
            //{"errcode":40013,"errmsg":"invalid appid"}
        }
        /// <summary>
        /// 获取关注者OpenId信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public static OpenIdResultJson List(string accessToken, string nextOpenId)
        {
            //微信公众平台获取关注者列表接口地址
            string urlFormat = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}";
            if (!string.IsNullOrEmpty(nextOpenId))
            {
                urlFormat += "&next_openid={1}";
                return ApiHelper.Get<OpenIdResultJson>(accessToken, urlFormat, nextOpenId);
            }
            else
                return ApiHelper.Get<OpenIdResultJson>(accessToken, urlFormat);
        }
    }
}
