using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample_3.ORM;

namespace Sample_3
{
    public class ReservationDll : BaseDll<Reservation>
    {
        /// <summary>
        /// 本演示程序仅保存一个预约表单
        /// </summary>
        const int ID = 1;
        /// <summary>
        /// 添加预约表单
        /// </summary>
        /// <param name="entity">预约表单</param>
        /// <returns>返回0表示添加失败</returns>
        public int AddReservation(Reservation entity)
        {
            var temp = base.LoadEntity(p => p.ID == ID);
            if (temp != null)
            {
                temp.Name = entity.Name;
                if (base.UpdateEntity(temp))
                    return ID;
                else
                    return 0;
            }
            else
            {
                return base.AddEntity(entity).ID;
            }
        }
    }
}