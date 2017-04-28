using System;
using System.ComponentModel.DataAnnotations;
using Sample_5.ORM;

namespace Sample_5.ViewEntity
{
    /// <summary>
    /// 渠道类型
    /// </summary>
    [Serializable]
    public class ChannelTypeEntity : IViewModel<ChannelTypeEntity, ChannelType>
    {
        static ChannelTypeEntity()
        {
            AutoMapper.Mapper.CreateMap<ChannelType, ChannelTypeEntity>();
            AutoMapper.Mapper.CreateMap<ChannelTypeEntity, ChannelType>();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 渠道类型名称
        /// </summary>
        public string Name { get; set; }
        public ChannelTypeEntity GetViewModel(ChannelType entity)
        {
            return AutoMapper.Mapper.Map<ChannelType, ChannelTypeEntity>(entity);
        }
        public ChannelType GetDataEntity(ChannelTypeEntity entity)
        {
            return AutoMapper.Mapper.Map<ChannelTypeEntity, ChannelType>(entity);
        }
    }
}