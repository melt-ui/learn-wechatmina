using System;
using System.ComponentModel.DataAnnotations;
using Sample_3.ORM;

namespace Sample_3
{
    /// <summary>
    /// 预约表单
    /// </summary>
    [Serializable]
    public class ReservationEntity : IViewModel<ReservationEntity, Reservation>
    {
        static ReservationEntity()
        {
            AutoMapper.Mapper.CreateMap<Reservation, ReservationEntity>();
            AutoMapper.Mapper.CreateMap<ReservationEntity, Reservation>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 预约表单名称
        /// </summary>
        public string Name { get; set; }

        public ReservationEntity GetViewModel(Reservation entity)
        {
            return AutoMapper.Mapper.Map<Reservation, ReservationEntity>(entity);
        }

        public Reservation GetDataEntity(ReservationEntity entity)
        {
            return AutoMapper.Mapper.Map<ReservationEntity, Reservation>(entity);
        }
    }
}