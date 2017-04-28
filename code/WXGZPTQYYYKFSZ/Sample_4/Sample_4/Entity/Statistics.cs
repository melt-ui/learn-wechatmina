using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample_4
{
    /// <summary>
    /// 访问与分享记录
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// 页面访问记录
        /// </summary>
        public List<PageNavEntity> PageNav { get; set; }
        /// <summary>
        /// 页面分享记录
        /// </summary>
        public List<PageShareEntity> PageShare { get; set; }
    }
}