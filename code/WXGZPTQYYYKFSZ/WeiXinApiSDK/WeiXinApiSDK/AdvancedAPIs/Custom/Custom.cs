using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 客服接口
    /// </summary>
    public static class CustomerService
    {
        /// <summary>
        /// 微信公众平台客服接口地址
        /// </summary>
        private const string URL_FORMAT = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">接收消息的用户OpenId</param>
        /// <param name="content">要发送的文字信息</param>
        /// <returns></returns>
        public static WxJsonResult SendText(string accessToken, string openId, string content)
        {
            var data = new
            {
                touser = openId,
                msgtype = "text",
                text = new
                {
                    content = content
                }
            };
            return ApiHelper.Post(accessToken, URL_FORMAT, data);
        }
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">接收消息的用户OpenId</param>
        /// <param name="mediaId">已上传到微信的图片文件ID</param>
        /// <returns></returns>
        public static WxJsonResult SendImage(string accessToken, string openId, string mediaId)
        {
            var data = new
            {
                touser = openId,
                msgtype = "image",
                image = new
                {
                    media_id = mediaId
                }
            };
            return ApiHelper.Post(accessToken, URL_FORMAT, data);
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">接收消息的用户OpenId</param>
        /// <param name="mediaId">已上传到微信的语音文件ID</param>
        /// <returns></returns>
        public static WxJsonResult SendVoice(string accessToken, string openId, string mediaId)
        {
            var data = new
            {
                touser = openId,
                msgtype = "voice",
                voice = new
                {
                    media_id = mediaId
                }
            };
            return ApiHelper.Post(accessToken, URL_FORMAT, data);
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">接收消息的用户OpenId</param>
        /// <param name="mediaId">已上传到微信的视频文件ID</param>
        /// <param name="title">视频消息的标题（非必须）</param>
        /// <param name="description">视频消息的描述（非必须）</param>
        /// <returns></returns>
        public static WxJsonResult SendVideo(string accessToken, string openId, string mediaId, string title, string description)
        {
            var data = new
            {
                touser = openId,
                msgtype = "video",
                video = new
                {
                    media_id = mediaId,
                    title = title,
                    description = description
                }
            };
            return ApiHelper.Post(accessToken, URL_FORMAT, data);
        }
        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">接收消息的用户OpenId</param>
        /// <param name="title">音乐标题（非必须）</param>
        /// <param name="description">音乐描述（非必须）</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumbMediaId">缩略图的媒体ID</param>
        /// <returns></returns>
        public static WxJsonResult SendMusic(string accessToken, string openId, string title, string description,
                                    string musicUrl, string hqMusicUrl, string thumbMediaId)
        {
            var data = new
            {
                touser = openId,
                msgtype = "music",
                music = new
                {
                    title = title,
                    description = description,
                    musicurl = musicUrl,
                    hqmusicurl = hqMusicUrl,
                    thumb_media_id = thumbMediaId
                }
            };
            return ApiHelper.Post(accessToken, URL_FORMAT, data);
        }
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">接收消息的用户OpenId</param>
        /// <param name="articles">图文消息实体，图文消息条数限制在10条以内</param>
        /// <returns></returns>
        public static WxJsonResult SendNews(string accessToken, string openId, List<Article> articles)
        {
            var data = new
            {
                touser = openId,
                msgtype = "news",
                news = new
                {
                    articles = articles.Select(z => new
                    {
                        title = z.Title,//标题
                        description = z.Description,//描述
                        url = z.Url,//点击后跳转的链接
                        picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                    }).ToList()
                }
            };
            return ApiHelper.Post(accessToken, URL_FORMAT, data);
        }
    }
}
