using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample_3.ORM;

namespace Sample_3
{
    public class ReservationContentDll : BaseDll<ReservationContent>
    {
        /// <summary>
        /// 添加预约表单字段
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>返回false表示添加失败</returns>
        public bool AddReservationContent(List<ReservationContent> entities)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentNullException("entities不能为空");
            int reservationID = entities.First().ReservationID;
            //获取原有预约表单字段
            var oldEntities = base.LoadEntities(p => p.ReservationID == reservationID).ToList();
            foreach (var entity in oldEntities)
            {
                //删除任一原有预约表单字段失败，则操作失败
                if (!base.DeleteEntity(entity))
                    return false;
            }
            foreach (var entity in entities)
            {
                //添加任一新预约表单字段失败，则操作失败
                if (base.AddEntity(entity).ID <= 0)
                    return false;
            }
            return true;
        }
    }
}