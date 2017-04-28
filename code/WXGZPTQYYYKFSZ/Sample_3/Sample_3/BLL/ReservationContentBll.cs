using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample_3
{
    public class ReservationContentBll
    {
        /// <summary>
        /// 获取预约表单字段实体
        /// </summary>
        /// <param name="reservationID">预约ID</param>
        /// <returns></returns>
        public List<ReservationContentEntity> GetReservationContents(int reservationID)
        {
            var dataEntities = new ReservationContentDll().LoadEntities(p => p.ReservationID == reservationID).ToList();
            //转换为ViewEntity
            var viewEntity = new ReservationContentEntity();
            return dataEntities.Select(p=>viewEntity.GetViewModel(p)).ToList();
        }
    }
}