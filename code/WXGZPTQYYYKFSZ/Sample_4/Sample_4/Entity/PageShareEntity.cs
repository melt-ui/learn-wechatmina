using System;
namespace Sample_4
{
    /// <summary>
    /// 页面分享记录
    /// </summary>
    public class PageShareEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 页面地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 分享类型
        /// </summary>
        public ShareType From { get; set; }

        /// <summary>
        /// 分享者微信openid
        /// </summary>
        public string ShareOpenId { get; set; }

        /// <summary>
        /// 上一级分享者微信openid
        /// </summary>
        public string ParentShareOpenId { get; set; }

        /// <summary>
        /// 分享时间
        /// </summary>
        public DateTime ShareTime { get; set; }
    }
}