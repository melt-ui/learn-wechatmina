using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Sample_5.DAL;
using Sample_5.ORM;
using Sample_5.ViewEntity;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;

namespace Sample_5.BLL
{
    public class ChannelBll
    {
        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        public List<ChannelEntity> GetEntities()
        {
            var entities = new ChannelDll().LoadEntities(p => p.ID > 0).ToList();
            var viewEntity = new ChannelEntity();
            return entities.Select(p => viewEntity.GetViewModel(p)).ToList();
        }

        /// <summary>
        /// 根据ID获取渠道
        /// </summary>
        /// <param name="id">渠道ID</param>
        /// <returns></returns>
        public ChannelEntity GetEntityById(int id)
        {
            var entity = new ChannelDll().LoadEntity(p => p.ID == id);
            var viewEntity = new ChannelEntity();
            return viewEntity.GetViewModel(entity);
        }

        /// <summary>
        /// 添加或修改渠道
        /// </summary>
        /// <param name="viewEntity">渠道实体</param>
        /// <returns></returns>
        public bool UpdateOrInsertEntity(ChannelEntity viewEntity)
        {
            if (viewEntity.ID > 0)
            {
                var entity = viewEntity.GetDataEntity(viewEntity);
                var dbEntity = new ChannelDll().LoadEntity(p => p.ID == entity.ID);
                entity.SceneId = dbEntity.SceneId;
                entity.Qrcode = dbEntity.Qrcode;
                return new ChannelDll().UpdateEntity(entity);
            }
            else
            {                
                //新增渠道时，需要获取渠道的二维码
                GetQrcode(viewEntity);
                var entity = viewEntity.GetDataEntity(viewEntity);
                return new ChannelDll().AddEntity(entity).ID > 0;
            }
        }

        /// <summary>
        /// 根据ID删除渠道
        /// </summary>
        /// <param name="id">渠道ID</param>
        /// <returns></returns>
        public bool DeleteEntityById(int id)
        {
            var entity = new ChannelDll().LoadEntity(p => p.ID == id);
            return new ChannelDll().DeleteEntity(entity);
        } 

        /// <summary>
        /// 根据SceneId获取二维码id
        /// </summary>
        /// <param name="sceneId">扫描的二维码的参数</param>
        /// <returns></returns>
        public int GetChannelIdBySceneId(int sceneId)
        {
            var entity = new ChannelDll().LoadEntity(p=>p.SceneId == sceneId);
            return entity == null ? 0 : entity.ID;
        }

        /// <summary>
        /// 判断渠道名称是否存在
        /// </summary>
        /// <param name="channelName">渠道名称</param>
        /// <param name="id">渠道ID</param>
        /// <returns></returns>
        public bool IsExitChannelName(string channelName, int id)
        {
            return new ChannelDll().IsExitChannelName(channelName, id);
        }

        /// <summary>
        /// 获取渠道的二维码
        /// </summary>
        /// <param name="channelName">渠道实体</param>
        /// <returns></returns>
        private void GetQrcode(ChannelEntity entity)
        {
            //获取微信公众平台接口访问凭据
            string accessToken = AccessTokenContainer.TryGetToken(ConfigurationManager.AppSettings["appID"], ConfigurationManager.AppSettings["appsecret"]);
            //找出一个未被使用的场景值ID，确保不同渠道使用不同的场景值ID
            int scenid = GetNotUsedSmallSceneId();
            if (scenid <= 0 || scenid > 100000)
            {
                throw new Exception("抱歉，您的二维码已经用完，请删除部分后重新添加");
            }
            CreateQrCodeResult createQrCodeResult = QrCode.Create(accessToken, 0, scenid);
            if (!string.IsNullOrEmpty(createQrCodeResult.ticket))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    //根据ticket获取二维码
                    QrCode.ShowQrCode(createQrCodeResult.ticket, stream);
                    //将获取到的二维码图片转换为Base64String格式
                    byte[] imageBytes = stream.ToArray();
                    string base64Image = System.Convert.ToBase64String(imageBytes);
                    //由于SqlServerCompact数据库限制最长字符4000，本测试项目将二维码保存到磁盘，正式项目中可直接保存到数据库
                    string imageFile = "QrcodeImage" + scenid.ToString() + ".img";
                    File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/") + imageFile, base64Image);
                    entity.Qrcode = imageFile;
                    entity.SceneId = scenid;
                }
            }
            else
            {
                throw new Exception("抱歉！获取二维码失败");
            }
        }

        /// <summary>
        /// 找出没有用的最小SceneId
        /// </summary>
        /// <returns></returns>
        private int GetNotUsedSmallSceneId()
        {
            var listSceneId = new ChannelDll().LoadEntities(p => p.ID > 0).Select(p => p.SceneId).OrderBy(p => p);
            for (int i = 1; i <= 100000; i++)
            {
                var sceneId = listSceneId.Any(e => e == i);
                if (!sceneId)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}