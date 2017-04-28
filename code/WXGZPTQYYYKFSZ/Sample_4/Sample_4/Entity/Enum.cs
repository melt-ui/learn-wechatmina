namespace Sample_4
{
    /// <summary>
    /// 访问来源类型
    /// </summary>
    public enum NavFrom
    {
        /// <summary>
        /// 微信朋友圈
        /// </summary>
        Timeline,
        /// <summary>
        /// 微信好友发送的链接
        /// </summary>
        Message,
        /// <summary>
        /// 直接在微信公众号中打开微信浏览器
        /// </summary>
        MicroMessenger,
        /// <summary>
        /// 其他（不是在微信中访问）
        /// </summary>
        Other
    }

    /// <summary>
    /// 分享类型
    /// </summary>
    public enum ShareType
    {
        /// <summary>
        /// 分享给好友
        /// </summary>
        Friend,
        /// <summary>
        /// 分享到朋友圈
        /// </summary>
        Timeline,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown
    }
}