using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum OAuthScope
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户openid
        /// </summary>
        snsapi_base,
        /// <summary>
        /// 弹出授权页面，可通过openid拿到昵称、性别、所在地。
        /// 并且，即使在未关注的情况下，只要用户授权，也能获取其信息
        /// </summary>
        snsapi_userinfo
    }
    /// <summary>
    /// OAuth2.0网页授权接口
    /// </summary>
    public static class OAuth
    {
        /// <summary>
        /// 第一步：生成网页授权访问地址
        /// </summary>
        /// <param name="appId">公众号的唯一标识</param>
        /// <param name="redirectUrl">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="state">重定向后会带上state参数，可以填写a-zA-Z0-9的参数值</param>
        /// <param name="scope">应用授权作用域</param>
        /// <param name="responseType">返回类型，目前只有code一种</param>
        /// <returns>网页授权访问地址，引导关注者打开该地址进入网页授权</returns>
        public static string GetAuthorizeUrl(string appId, string redirectUrl, string state, OAuthScope scope, string responseType = "code")
        {
            var url =
                string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect",
                                appId, System.Web.HttpUtility.UrlEncode(redirectUrl), responseType, scope, state);
            return url;
        }
        /// <summary>
        /// 第二步：获取AccessToken
        /// 用户访问第一步网页授权页面后，无论同意或拒绝，都会返回redirectUrl页面。
        /// 如果用户同意授权，页面将跳转至 redirect_uri/?code=CODE&state=STATE。
        /// 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
        /// </summary>
        /// <param name="appId">公众号的appid</param>
        /// <param name="secret">公众号的appsecret</param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，
        /// code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="grantType">目前只有authorization_code一种</param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetAccessToken(string appId, string secret, string code, string grantType = "authorization_code")
        {
            var url =
                string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type={3}",
                                appId, secret, code, grantType);
            string result = RequestUtility.HttpGet(url, null);
            return ApiHelper.GetResult<OAuthAccessTokenResult>(result);
        }
        /// <summary>
        /// 第三步：刷新access_token（如果需要）
        /// 由于access_token拥有较短的有效期，当access_token超时后，可以使用refresh_token进行刷新，
        /// refresh_token拥有较长的有效期（7天、30天、60天、90天），当refresh_token失效的后，需要用户重新授权。
        /// </summary>
        /// <param name="appId">公众号的appid</param>
        /// <param name="refreshToken">填写通过第二步获取到的refresh_token参数</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public static OAuthAccessTokenResult RefreshToken(string appId, string refreshToken, string grantType = "refresh_token")
        {
            var url =
                string.Format("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid={0}&grant_type={1}&refresh_token={2}",
                                appId, grantType, refreshToken);
            string result = RequestUtility.HttpGet(url, null);
            return ApiHelper.GetResult<OAuthAccessTokenResult>(result);
        }
        /// <summary>
        /// 第四步：拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">要获取用信息的用户OpenId</param>
        /// <returns></returns>
        public static OAuthUserInfo GetUserInfo(string accessToken,string openId)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}",accessToken,openId);
            string result = RequestUtility.HttpGet(url, null);
            return ApiHelper.GetResult<OAuthUserInfo>(result);
        }
    }
}
