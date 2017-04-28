using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 二维码接口
    /// </summary>
    public static class QrCode
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="expireSeconds">该二维码有效时间，以秒为单位。 最大不超过1800。0时为永久二维码</param>
        /// <param name="sceneId">场景值ID，临时二维码时为32位整型，永久二维码时最大值为1000</param>
        /// <returns></returns>
        public static CreateQrCodeResult Create(string accessToken, int expireSeconds, int sceneId)
        {
            //微信公众平台创建二维码的接口地址
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";
            object data = null;
            if (expireSeconds > 0)
            {
                //临时二维码
                data = new
                {
                    expire_seconds = expireSeconds,
                    action_name = "QR_SCENE",
                    action_info = new
                    {
                        scene = new
                        {
                            scene_id = sceneId
                        }
                    }
                };
            }
            else
            {
                //永久二维码
                data = new
                {
                    action_name = "QR_LIMIT_SCENE",
                    action_info = new
                    {
                        scene = new
                        {
                            scene_id = sceneId
                        }
                    }
                };
            }
            return ApiHelper.Post<CreateQrCodeResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 获取二维码（不需要AccessToken）
        /// 错误情况下（如ticket非法）返回HTTP错误码404。
        /// </summary>
        /// <param name="ticket">调用Create获取到的ticket</param>
        /// <param name="stream">获取到的二维码图片</param>
        public static void ShowQrCode(string ticket, Stream stream)
        {
            //微信公众平台通过ticket换取二维码的接口地址
            var urlFormat = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";
            ApiHelper.Download(urlFormat, stream, ticket);
        }
    }
}
