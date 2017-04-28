using System;
namespace Sample_4
{
    /// <summary>
    /// 页面访问记录
    /// </summary>
    public class PageNavEntity
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
        /// 访问来源
        /// </summary>
        public NavFrom From { get; set; }

        /// <summary>
        /// 访问者微信openid
        /// </summary>
        public string NavOpenId { get; set; }

        /// <summary>
        /// 当访问来源为朋友圈时的分享者微信openid
        /// </summary>
        public string ShareOpenId { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime VisitTime { get; set; }
    }
}