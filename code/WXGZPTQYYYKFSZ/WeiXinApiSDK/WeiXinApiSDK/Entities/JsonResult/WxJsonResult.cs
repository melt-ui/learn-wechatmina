using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 微信接口返回的结果
    /// 正确时的返回JSON数据包如下：
    /// {"errcode":0,"errmsg":"ok"}
    /// </summary>
    public class WxJsonResult
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        public  ReturnCode errcode { get; set; }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string errmsg { get; set; }
    }
}
