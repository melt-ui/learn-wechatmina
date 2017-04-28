using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample_5.ORM;

namespace Sample_5.DAL
{
    /// <summary>
    /// 微信用户信息表的数据访问类
    /// </summary>
    public class WeixinUserInfoDll : BaseDll<WeixinUserInfo>
    {
        /// <summary>
        /// 根据OpenId删除微信用户信息
        /// </summary>
        /// <param name="openId">微信用户OpenId</param>
        /// <returns></returns>
        public bool DeleteByOpenId(string openId)
        {
            var entity = LoadEntity(p => p.OpenId == openId);
            if (entity != null)
            {
                return DeleteEntity(entity);
            }
            else
                return false;
        }
        
        /// <summary>
        /// 插入微信用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public void Insert(WeixinUserInfo userInfo)
        {
            AddEntity(userInfo);
        }

        /// <summary>
        /// 更新微信用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public void Update(WeixinUserInfo userInfo)
        {
            var entity = new WeixinUserInfoDll().LoadEntity(p => p.OpenId == userInfo.OpenId);
            userInfo.ID = entity.ID;
            UpdateEntity(userInfo);
        }
    }
}