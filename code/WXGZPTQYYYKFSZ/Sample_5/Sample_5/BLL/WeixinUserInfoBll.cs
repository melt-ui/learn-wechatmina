using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using Sample_5.DAL;
using Sample_5.ORM;
using Sample_5.ViewEntity;

namespace Sample_5.BLL
{
    public class WeixinUserInfoBll
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static WeixinUserInfoBll()
        {
            WeixinUserInfoSynchronize.Synchronize();
        }

        /// <summary>
        /// 获取微信用户信息列表
        /// </summary>
        /// <returns></returns>
        public List<WeixinUserInfoEntity> GetEntities()
        {
            var entities = new WeixinUserInfoDll().LoadEntities(p => p.OpenId != "").ToList();
            var viewEntity = new WeixinUserInfoEntity();
            return entities.Select(p => viewEntity.GetViewModel(p)).ToList();
        }
    }
}