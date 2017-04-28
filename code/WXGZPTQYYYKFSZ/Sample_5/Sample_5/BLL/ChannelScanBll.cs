using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Sample_5.DAL;
using Sample_5.ViewEntity;

namespace Sample_5.BLL
{
    public class ChannelScanBll
    {
        /// <summary>
        /// 保存扫描记录
        /// </summary>
        /// <param name="openId">微信用户OpenId</param>
        /// <param name="sceneId">扫描的二维码的参数</param>
        /// <param name="scanType">扫描类型</param>
        public void SaveScan(string openId, int sceneId, ScanType scanType)
        {		   
            //微信公众平台要求微信公众号服务器必须在5秒内返回相应结果，否则会重新发送请求，一共重试三次
            //为了避免微信公众号服务器重复接收到同一条扫描记录，造成数据重复，导致统计失真，这里将保存扫描记录的操作放到线程池中异步执行，尽快返回相应结果给微信服务器
            ThreadPool.QueueUserWorkItem(e =>
            {
                int channelId = new ChannelBll().GetChannelIdBySceneId(sceneId);
                if (channelId <= 0)
                {
                    return;
                }
                ChannelScanEntity entity = new ChannelScanEntity()
                {
                    ChannelId = channelId,
                    ScanTime = DateTime.Now,
                    OpenId = openId,
                    ScanType = scanType
                };
                new ChannelScanDll().AddEntity(entity.GetDataEntity(entity));
            });
        }

        /// <summary>
        /// 获取渠道的扫描记录
        /// </summary>
        /// <param name="channelId">渠道ID</param>
        /// <returns></returns>
        public List<ChannelScanDisplayEntity> GetChannelScanList(int channelId)
        {
            //获取渠道扫描记录
            var entities = new ChannelScanDll().LoadEntities(p => p.ChannelId == channelId).ToList();
            var viewEntity = new ChannelScanEntity();
            var result = entities.Select(p => new ChannelScanDisplayEntity() { ScanEntity = viewEntity.GetViewModel(p) }).ToList();
            //获取每条渠道扫描记录对应的微信用户信息
            var openIds = result.Select(p=>p.ScanEntity.OpenId).ToArray();
            //在渠道扫描记录中包含微信用户信息，便于前端页面显示
            var userinfoEntities = new WeixinUserInfoDll().LoadEntities(p => openIds.Contains(p.OpenId)).ToList();
            var userinfoViewEntity = new WeixinUserInfoEntity();
            var userinfoViewEnities = userinfoEntities.Select(p => userinfoViewEntity.GetViewModel(p)).ToList();
            result.ForEach(e=>{
                e.UserInfoEntity = userinfoViewEnities.Where(p => p.OpenId == e.ScanEntity.OpenId).FirstOrDefault();
            });
            return result;
        }
    }
}