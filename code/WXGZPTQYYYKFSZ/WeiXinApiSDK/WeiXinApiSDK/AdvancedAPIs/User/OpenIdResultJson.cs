using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 关注者列表返回结果
    /// </summary>
    public class OpenIdResultJson : WxJsonResult
    {
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
       public int total { get; set; }
        /// <summary>
       /// 拉取的OPENID个数，最大值为10000
        /// </summary>
       public int count { get; set; }
        /// <summary>
       /// OPENID的列表
        /// </summary>
       public OpenIdResultJson_Data data { get; set; }
        /// <summary>
       /// 拉取列表的后一个用户的OPENID
        /// </summary>
       public string next_openid { get; set; }
    }
    /// <summary>
    /// 关注者OPENID列表
    /// </summary>
    public class OpenIdResultJson_Data
    {
        public List<string> openid { get; set; }
    }
}
