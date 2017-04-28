using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using Sample_5.DAL;
using Sample_5.ORM;
using Sample_5.ViewEntity;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;

namespace Sample_5.BLL
{
    /// <summary>
    /// 同步微信用户信息
    /// 静态类，保证跨线程全局唯一的
    /// </summary>
    public static class WeixinUserInfoSynchronize
    {
        private delegate void GetExecute(WeixinUserInfo userInfo);
        /// <summary>
        /// 同步微信用户信息线程
        /// </summary>
        private static Thread SynchronizeWeixinUserThread = null;
        /// <summary>
        /// 锁
        /// </summary>
        private static object lockSingal = new object(); 

        /// <summary>
        /// 开启同步微信用户信息线程
        /// 单例模式
        /// </summary>
        public static void Synchronize()
        {
            if (SynchronizeWeixinUserThread == null)
            {
                lock(lockSingal)
                {
                    if (SynchronizeWeixinUserThread == null)
                    {
                        // 开启同步微信用户信息的后台线程
                        ThreadStart start = new ThreadStart(SynchronizeWeixinUserCircle);
                        SynchronizeWeixinUserThread = new Thread(start);
                        SynchronizeWeixinUserThread.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 每隔60秒执行一次微信用户信息同步方法
        /// </summary>
        private static void SynchronizeWeixinUserCircle()
        {
            try
            {
                SynchronizeWeixinUser();
                Thread.Sleep(60000);
            }
            catch { }
        }

        /// <summary>
        /// 微信用户信息同步方法
        /// </summary>
        /// <returns></returns>
        private static void SynchronizeWeixinUser()
        {
            OpenIdResultJson weixinOpenIds = GetAllOpenIds();

            //获取已同步到数据库中的微信用户的OpenId
            List<string> dataOpenList = new WeixinUserInfoDll().LoadEntities(p => p.ID > 0).Select(e => e.OpenId).ToList();
            List<string> insertOpenIdList = new List<string>();
            List<string> updateOpenIdList = new List<string>();
            List<string> deleteOpenIdList = new List<string>();
            //判断每个微信用户需要执行的操作
            for (int index = 0; index < weixinOpenIds.data.openid.Count; index++)
            {
                var weixinOpenId = weixinOpenIds.data.openid[index];
                var user = dataOpenList.Find(e => e == weixinOpenId);
                if (user == null)
                {
                    //不存在数据库中的，插入
                    insertOpenIdList.Add(weixinOpenId);
                }
                else
                {
                    //已存在数据库中的，修改
                    updateOpenIdList.Add(weixinOpenId);
                }
            }
            //已取消关注该微信公众号的，删除
            insertOpenIdList.ForEach(e => dataOpenList.Remove(e));
            updateOpenIdList.ForEach(e => dataOpenList.Remove(e));
            deleteOpenIdList.AddRange(dataOpenList);

            //插入失败的openId列表，用于失败重试
            List<string> failedInsert = new List<string>();
            //修改失败的openId列表，用于失败重试
            List<string> failedUpdate = new List<string>();
            //插入新的微信用户
            foreach (var openId in insertOpenIdList)
            {
                ExecuteWeixinUser(openId, new WeixinUserInfoDll().Insert, failedInsert);
            }
            //更新已有微信用户
            foreach (var openId in updateOpenIdList)
            {
                ExecuteWeixinUser(openId, new WeixinUserInfoDll().Update, failedUpdate);
            }
            if (deleteOpenIdList.Count > 0)
            {
                //删除已取消关注该微信公众号的微信用户
                foreach (var openId in deleteOpenIdList)
                {
                    new WeixinUserInfoDll().DeleteByOpenId(openId);
                }
            }
            //插入失败，重试一次
            if (failedInsert.Count > 0)
            {
                List<string> fail = new List<string>();
                foreach (var openId in failedInsert)
                {
                    ExecuteWeixinUser(openId, new WeixinUserInfoDll().Insert, fail);
                }
            }
            //更新失败，重试一次
            if (failedUpdate.Count > 0)
            {
                List<string> fail = new List<string>();
                foreach (var openId in failedInsert)
                {
                    ExecuteWeixinUser(openId, new WeixinUserInfoDll().Update, fail);
                }
            }
        }

        /// <summary>
        /// 获取所有关注者的OpenId信息
        /// </summary>
        /// <returns></returns>
        private static OpenIdResultJson GetAllOpenIds()
        {
            string accessToken = AccessTokenContainer.TryGetToken(ConfigurationManager.AppSettings["appID"], ConfigurationManager.AppSettings["appsecret"]);
            OpenIdResultJson openIdResult = User.List(accessToken, null);
            while (!string.IsNullOrWhiteSpace(openIdResult.next_openid))
            {
                OpenIdResultJson tempResult = User.List(accessToken, openIdResult.next_openid);
                openIdResult.next_openid = tempResult.next_openid;
                if (tempResult.data != null && tempResult.data.openid != null)
                {
                    openIdResult.data.openid.AddRange(tempResult.data.openid);
                }
            }
            return openIdResult;
        }

        /// <summary>
        /// 获取openId对应的用户信息并存入数据库
        /// </summary>
        /// <param name="openId">微信用户openId</param>
        /// <param name="execute">修改、删除或插入操作</param>
        /// <param name="failList">未成功获取到用户信息的openId列表</param>
        private static void ExecuteWeixinUser(string openId, GetExecute execute, List<string> failList)
        {
            string accessToken = AccessTokenContainer.TryGetToken(ConfigurationManager.AppSettings["appID"], ConfigurationManager.AppSettings["appsecret"]);
            var userInfo = User.Info(accessToken, openId);
            if (userInfo.errcode != ReturnCode.请求成功)
            {
                failList.Add(openId);
            }
            else
            {
                WeixinUserInfo entity = new WeixinUserInfo()
                {
                    City = userInfo.city,
                    Province = userInfo.province,
                    Country = userInfo.country,
                    HeadImgUrl = userInfo.headimgurl,
                    Language = userInfo.language,
                    Subscribe_time = userInfo.subscribe_time,
                    Sex = (Int16)userInfo.sex,
                    NickName = userInfo.nickname,
                    OpenId = userInfo.openid

                };
                execute(entity);
            }
        }
    }
}