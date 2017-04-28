using System;
namespace Sample_5.ViewEntity
{
    /// <summary>
    /// 用于前端口显示的渠道扫描记录
    /// </summary>
    [Serializable]
    public class ChannelScanDisplayEntity
    {
        /// <summary>
        /// 渠道扫描记录
        /// </summary>
        public ChannelScanEntity ScanEntity { get; set; }
        /// <summary>
        /// 扫描渠道的微信用户信息
        /// </summary>
        public WeixinUserInfoEntity UserInfoEntity { get; set; }
    }
}