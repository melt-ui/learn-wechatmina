using System;
using System.ComponentModel.DataAnnotations;
using Sample_3.ORM;

namespace Sample_3
{
    /// <summary>
    /// 预约表单字段
    /// </summary>
    [Serializable]
    public class ReservationContentEntity : IViewModel<ReservationContentEntity, ReservationContent>
    {
        static ReservationContentEntity()
        {
            AutoMapper.Mapper.CreateMap<ReservationContent, ReservationContentEntity>();
            AutoMapper.Mapper.CreateMap<ReservationContentEntity, ReservationContent>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 初始内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 预约表单ID
        /// </summary>
        public int ReservationID { get; set; }

        public ReservationContentEntity GetViewModel(ReservationContent entity)
        {
            return AutoMapper.Mapper.Map<ReservationContent, ReservationContentEntity>(entity);
        }

        public ReservationContent GetDataEntity(ReservationContentEntity entity)
        {
            return AutoMapper.Mapper.Map<ReservationContentEntity, ReservationContent>(entity);
        }
    }
}