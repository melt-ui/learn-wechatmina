using System;
using System.ComponentModel.DataAnnotations;
using Sample_5.ORM;

namespace Sample_5.ViewEntity
{
    /// <summary>
    /// 微信用户信息
    /// </summary>
    [Serializable]
    public class WeixinUserInfoEntity : IViewModel<WeixinUserInfoEntity, WeixinUserInfo>
    {
        static WeixinUserInfoEntity()
        {
            var map = AutoMapper.Mapper.CreateMap<WeixinUserInfo, WeixinUserInfoEntity>();
            map.ForMember(e => e.Sex, d => d.MapFrom(n => (Sex)n.Sex));
            var m1 = AutoMapper.Mapper.CreateMap<WeixinUserInfoEntity, WeixinUserInfo>();
            m1.ForMember(e => e.Sex, d => d.MapFrom(n => (short)n.Sex));
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 微信用户的OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 普通用户的头像链接
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 普通用户的语言，简体中文为zh_CN
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 微信服务器保存的用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public long Subscribe_time { get; set; }
        /// <summary>
        /// 项目中实际使用的用户关注时间
        /// </summary>
        public DateTime SubscribeTime
        {
            get
            {
                //微信服务器保存的用户关注时间时间戳，为从1970年1月1日0时起到用户关注时所经过的毫秒数
                //需要进行计算才能正确转换为C#中DateTime类型的时间
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(Subscribe_time + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                DateTime dtResult = dtStart.Add(toNow);
                return dtResult;
            }
        }
        public WeixinUserInfoEntity GetViewModel(WeixinUserInfo entity)
        {
            return AutoMapper.Mapper.Map<WeixinUserInfo, WeixinUserInfoEntity>(entity);
        }
        public WeixinUserInfo GetDataEntity(WeixinUserInfoEntity entity)
        {
            return AutoMapper.Mapper.Map<WeixinUserInfoEntity, WeixinUserInfo>(entity);
        }
    }
}