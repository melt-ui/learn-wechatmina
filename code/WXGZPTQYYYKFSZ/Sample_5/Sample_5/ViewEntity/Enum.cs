using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample_5.ViewEntity
{
    /// <summary>
    /// 渠道扫描类型
    /// </summary>
    public enum ScanType
    {
        /// <summary>
        /// 关注
        /// </summary>
        Subscribe = 1,
        /// <summary>
        /// 已关注后扫描
        /// </summary>
        Scan
    }
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown,
        /// <summary>
        /// 男
        /// </summary>
        Male,
        /// <summary>
        /// 女
        /// </summary>
        Female
    }
}