using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微信公众号多媒体文件接口
    /// </summary>
    public static class Media
    {
        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="type">上传文件类型</param>
        /// <param name="file">上传文件物理路径</param>
        /// <returns></returns>
        public static UploadResultJson Upload(string accessToken, UploadMediaFileType type, string file)
        {
            //微信公众号上传媒体文件接口地址
            var urlFormat = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            return ApiHelper.Upload<UploadResultJson>(accessToken, urlFormat, fileDictionary, type.ToString());
        }

        /// <summary>
        /// 下载媒体文件
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">媒体文件ID</param>
        /// <param name="stream">下载结果</param>
        public static void Get(string accessToken, string mediaId, Stream stream)
        {            
            //微信公众号下载媒体文件接口地址
            var urlFormat = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
            ApiHelper.Download(urlFormat, stream, accessToken, mediaId);
        }
    }
}
