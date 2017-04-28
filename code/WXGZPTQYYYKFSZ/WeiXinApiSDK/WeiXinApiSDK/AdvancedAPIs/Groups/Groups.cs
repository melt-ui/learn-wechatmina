using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 用户分组接口
    /// </summary>
    public static class Groups
    {
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="name">分组名称（30个字符以内）</param>
        /// <returns></returns>
        public static CreateGroupResult Create(string accessToken, string name)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";
            var data = new
            {
                group = new
                {
                    name = name
                }
            };
            return ApiHelper.Post<CreateGroupResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 获取分组列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GroupsJson Get(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";
            return ApiHelper.Get<GroupsJson>(accessToken, urlFormat);
        }
        /// <summary>
        /// 获取单个用户所属分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">用户OpenId</param>
        /// <returns></returns>
        public static GetGroupIdResult GetId(string accessToken, string openId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}";
            var data = new { openid = openId };
            return ApiHelper.Post<GetGroupIdResult>(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 修改分组名称
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="id">分组ID</param>
        /// <param name="name">新的分组名称（30个字符以内）</param>
        /// <returns></returns>
        public static WxJsonResult Update(string accessToken, int id, string name)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}";
            var data = new
            {
                group = new
                {
                    id = id,
                    name = name
                }
            };
            return ApiHelper.Post(accessToken, urlFormat, data);
        }
        /// <summary>
        /// 移动单个用户用户到指定分组
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId">用户OpenId</param>
        /// <param name="toGroupId">要移动到的分组ID</param>
        /// <returns></returns>
        public static WxJsonResult MemberUpdate(string accessToken, string openId, int toGroupId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";
            var data = new
            {
                openid = openId,
                to_groupid = toGroupId
            };
            return ApiHelper.Post(accessToken, urlFormat, data);
        }
    }
}
