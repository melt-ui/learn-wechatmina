using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample_3.ORM;

namespace Sample_3
{
    public class UserReservationContentDll : BaseDll<UserReservationContent>
    {
        /// <summary>
        /// 添加用户填写的预约表单字段内容
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>返回false表示添加失败</returns>
        public bool AddUserReservationContent(List<UserReservationContent> entities)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentNullException("entities不能为空");
            foreach (var entity in entities)
            {
                //添加任一用户填写的预约表单字段内容失败，则操作失败
                if (base.AddEntity(entity).ID <= 0)
                    return false;
            }
            return true;
        }
    }
}