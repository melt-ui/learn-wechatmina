using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 用户分组列表
    /// </summary>
    public class GroupsJson : WxJsonResult
    {
        public List<GroupsJson_Group> groups { get; set; }
    }
    /// <summary>
    /// 单个用户分组
    /// </summary>
    public class GroupsJson_Group
    {
        /// <summary>
        /// 分组id，由微信分配
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 分组名字（30个字符以内）
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 分组内用户数量
        /// 此属性在CreateGroupResult的Json数据中，创建结果中始终为0
        /// </summary>
        public int count { get; set; }
    }
}
