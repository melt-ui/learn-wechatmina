using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 获取微信access_token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string appid, string secret, string grant_type = "client_credential")
        {
            //微信公众平台获取access_token接口地址
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type, appid, secret);
            string result = RequestUtility.HttpGet(url, null);
            return ApiHelper.GetResult<AccessTokenResult>(result);
        }
    }
}
