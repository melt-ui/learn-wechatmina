using System;
using System.ComponentModel.DataAnnotations;
using Sample_5.ORM;

namespace Sample_5.ViewEntity
{
    /// <summary>
    /// 渠道
    /// </summary>
    [Serializable]
    public class ChannelEntity : IViewModel<ChannelEntity, Channel>
    {
        static ChannelEntity()
        {
            AutoMapper.Mapper.CreateMap<Channel, ChannelEntity>();
            AutoMapper.Mapper.CreateMap<ChannelEntity, Channel>();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </summary>
        public int SceneId { get; set; }
        /// <summary>
        /// 渠道名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所属渠道类型ID
        /// </summary>
        public int ChannelTypeId { get; set; }
        /// <summary>
        /// 渠道二维码
        /// </summary>
        public string Qrcode { get; set; }
        public ChannelEntity GetViewModel(Channel entity)
        {
            return AutoMapper.Mapper.Map<Channel, ChannelEntity>(entity);
        }
        public Channel GetDataEntity(ChannelEntity entity)
        {
            return AutoMapper.Mapper.Map<ChannelEntity, Channel>(entity);
        }
    }
}