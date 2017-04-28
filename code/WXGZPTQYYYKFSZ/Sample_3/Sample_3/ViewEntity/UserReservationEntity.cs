using System;
using System.ComponentModel.DataAnnotations;
using Sample_3.ORM;

namespace Sample_3
{
    /// <summary>
    /// 预约填表用户
    /// </summary>
    public class UserReservationEntity : IViewModel<UserReservationEntity, UserReservation>
    {
        static UserReservationEntity()
        {
            AutoMapper.Mapper.CreateMap<UserReservation, UserReservationEntity>();
            AutoMapper.Mapper.CreateMap<UserReservationEntity, UserReservation>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 微信个人用户OpenId
        /// </summary>
        public string WeixinOpenId { get; set; }

        /// <summary>
        /// 预约表单ID
        /// </summary>
        public int ReservationID { get; set; }

        public UserReservationEntity GetViewModel(UserReservation entity)
        {
            return AutoMapper.Mapper.Map<UserReservation, UserReservationEntity>(entity);
        }

        public UserReservation GetDataEntity(UserReservationEntity entity)
        {
            return AutoMapper.Mapper.Map<UserReservationEntity, UserReservation>(entity);
        }
    }
}