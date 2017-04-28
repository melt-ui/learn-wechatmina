using System;
using System.ComponentModel.DataAnnotations;
using Sample_5.ORM;

namespace Sample_5.ViewEntity
{    
    /// <summary>
    /// 渠道二维码扫描记录
    /// </summary>
    [Serializable]
    public class ChannelScanEntity : IViewModel<ChannelScanEntity, ChannelScan>
    {
        static ChannelScanEntity()
        {
            var map = AutoMapper.Mapper.CreateMap<ChannelScan, ChannelScanEntity>();
            map.ForMember(e => e.ScanType, d => d.MapFrom(n => (ScanType)n.ScanType));
            var m1 = AutoMapper.Mapper.CreateMap<ChannelScanEntity, ChannelScan>();
            m1.ForMember(e => e.ScanType, d => d.MapFrom(n => (short)n.ScanType));
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 扫描微信用户的OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 扫描类型
        /// </summary>
        public ScanType ScanType { get; set; }
        /// <summary>
        /// 扫描时间
        /// </summary>
        public DateTime ScanTime { get; set; }
        /// <summary>
        /// 所属渠道
        /// </summary>
        public int ChannelId { get; set; }
        public ChannelScanEntity GetViewModel(ChannelScan entity)
        {
            return AutoMapper.Mapper.Map<ChannelScan, ChannelScanEntity>(entity);
        }
        public ChannelScan GetDataEntity(ChannelScanEntity entity)
        {
            return AutoMapper.Mapper.Map<ChannelScanEntity, ChannelScan>(entity);
        }
    }
}