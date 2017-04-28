using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.Helpers
{
    public static class ApiHelper
    {
        /// <summary>
        /// 从微信公众平台API获取信息的公共方法
        /// </summary>
        /// <param name="urlFormat">API接口地址格式</param>
        /// <param name="accessToken">微信公众号访问授权AccessToken</param>
        /// <param name="querys">除了AccessToken还需要传递的其他参数</param>
        /// <returns></returns>
        public static WxJsonResult Get(string accessToken, string urlFormat, params string[] querys)
        {
            return Get<WxJsonResult>(urlFormat, accessToken, querys);
        }

        /// <summary>
        /// 从微信公众平台API获取信息的公共方法
        /// </summary>
        /// <param name="urlFormat">API接口地址格式</param>
        /// <param name="accessToken">微信公众号访问授权AccessToken</param>
        /// <param name="querys">除了AccessToken还需要传递的其他参数</param>
        /// <returns></returns>
        public static T Get<T>(string accessToken, string urlFormat, params string[] querys)
        {
            var url = GetApiUrl(urlFormat, accessToken, querys);
            string result = RequestUtility.HttpGet(url, null);
            return GetResult<T>(result);
        }

        /// <summary>
        /// 向微信公众平台API发送信息的公共方法
        /// </summary>
        /// <param name="urlFormat">API接口地址格式</param>
        /// <param name="accessToken">微信公众号访问授权AccessToken</param>
        /// <param name="data">POST提交的数据</param>
        /// <param name="querys">除了AccessToken还需要传递的其他参数</param>
        /// <returns></returns>
        public static WxJsonResult Post(string accessToken, string urlFormat, object data, params string[] querys)
        {
            return Post<WxJsonResult>(urlFormat, accessToken, data, querys);
        }

        /// <summary>
        /// 向微信公众平台API发送信息的公共方法
        /// </summary>
        /// <param name="urlFormat">API接口地址格式</param>
        /// <param name="accessToken">微信公众号访问授权AccessToken</param>
        /// <param name="data">POST提交的数据</param>
        /// <param name="querys">除了AccessToken还需要传递的其他参数</param>
        /// <returns></returns>
        public static T Post<T>(string accessToken, string urlFormat, object data, params string[] querys)
        {
            var url = GetApiUrl(urlFormat, accessToken, querys);
            SerializerHelper serializerHelper = new SerializerHelper();
            var jsonString = serializerHelper.GetJsonString(data);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                string result = RequestUtility.HttpPost(url, ms, null);
                return GetResult<T>(result);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="urlFormat">API接口地址格式</param>
        /// <param name="stream">下载的文件流信息</param>
        /// <param name="querys">需要传递的参数</param>
        public static void Download(string urlFormat, Stream stream, params string[] querys)
        {
            RequestUtility.Download(string.Format(urlFormat, querys), stream);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="urlFormat">API接口地址格式</param>
        /// <param name="accessToken">微信公众号访问授权AccessToken</param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="querys">除了AccessToken还需要传递的其他参数</param>
        public static T Upload<T>(string accessToken, string urlFormat, Dictionary<string, string> fileDictionary, params string[] querys)
        {
            var url = GetApiUrl(urlFormat, accessToken, querys);
            string returnText = HttpUtility.RequestUtility.Upload(url, fileDictionary);
            var result = GetResult<T>(returnText);
            return result;
        }

        /// <summary>
        /// 获取API返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                        string.Format("微信Post请求发生错误！错误代码：{0}，说明：{1}",
                                      (int)errorResult.errcode,
                                      errorResult.errmsg),
                        null, errorResult);
                }
            }

            T result = js.Deserialize<T>(returnText);
            return result;
        }

        /// <summary>
        /// 生成微信公众平台API访问地址
        /// </summary>
        /// <param name="urlFormat">API接口地址格式</param>
        /// <param name="accessToken">微信公众号访问授权AccessToken</param>
        /// <param name="querys">除了AccessToken还需要传递的其他参数</param>
        /// <returns></returns>
        private static string GetApiUrl(string urlFormat, string accessToken, string[] querys)
        {
            string[] args = new string[] { accessToken };
            if (querys.Length > 0)
                args = args.Concat(querys).ToArray();
            var url = string.Format(urlFormat, args);
            return url;
        }
    }
}
