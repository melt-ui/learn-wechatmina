using System;
using System.ComponentModel.DataAnnotations;
using Sample_3.ORM;

namespace Sample_3
{
    /// <summary>
    /// 用户填写的表单信息
    /// </summary>
    public class UserReservationContentEntity : IViewModel<UserReservationContentEntity, UserReservationContent>
    {
        static UserReservationContentEntity()
        {
            AutoMapper.Mapper.CreateMap<UserReservationContent, UserReservationContentEntity>();
            AutoMapper.Mapper.CreateMap<UserReservationContentEntity, UserReservationContent>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 字段填写内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 填表用户ID
        /// </summary>
        public int UserReservationId { get; set; }

        /// <summary>
        /// 预约表单字段ID
        /// </summary>
        public int ReservationContentId { get; set; }

        public UserReservationContentEntity GetViewModel(UserReservationContent entity)
        {
            return AutoMapper.Mapper.Map<UserReservationContent, UserReservationContentEntity>(entity);
        }

        public UserReservationContent GetDataEntity(UserReservationContentEntity entity)
        {
            return AutoMapper.Mapper.Map<UserReservationContentEntity, UserReservationContent>(entity);
        }
    }
}