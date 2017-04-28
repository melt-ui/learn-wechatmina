using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample_5.DAL;
using Sample_5.ViewEntity;

namespace Sample_5.BLL
{
    public class ChannelTypeBll
    {
        /// <summary>
        /// 获取渠道类型列表
        /// </summary>
        /// <returns></returns>
        public List<ChannelTypeEntity> GetEntities()
        {
            var entities = new ChannelTypeDll().LoadEntities(p => p.ID > 0).ToList();
            var viewEntity = new ChannelTypeEntity();
            return entities.Select(p => viewEntity.GetViewModel(p)).ToList();
        }

        /// <summary>
        /// 根据ID获取渠道类型
        /// </summary>
        /// <param name="id">渠道类型ID</param>
        /// <returns></returns>
        public ChannelTypeEntity GetEntityById(int id)
        {
            var entity = new ChannelTypeDll().LoadEntity(p => p.ID == id);
            var viewEntity = new ChannelTypeEntity();
            return viewEntity.GetViewModel(entity);
        }

        /// <summary>
        /// 添加或修改渠道类型
        /// </summary>
        /// <param name="viewEntity">渠道类型实体</param>
        /// <returns></returns>
        public bool UpdateOrInsertEntity(ChannelTypeEntity viewEntity)
        {
            var entity = viewEntity.GetDataEntity(viewEntity);
            if (entity.ID > 0)
            {
                return new ChannelTypeDll().UpdateEntity(entity);
            }
            else
            {
                return new ChannelTypeDll().AddEntity(entity).ID > 0;
            }
        }

        /// <summary>
        /// 根据ID删除渠道类型
        /// </summary>
        /// <param name="id">渠道类型ID</param>
        /// <returns></returns>
        public bool DeleteEntityById(int id)
        {
            var entity = new ChannelTypeDll().LoadEntity(p => p.ID == id);
            return new ChannelTypeDll().DeleteEntity(entity);
        }
    }
}