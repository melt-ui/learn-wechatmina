using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Sample_3.ORM;

namespace Sample_3
{
    public class ReservationBll
    {        
        /// <summary>
        /// 本演示程序仅保存一个预约表单
        /// </summary>
        const int ID = 1;

        /// <summary>
        /// 获取预约实体
        /// </summary>
        /// <returns></returns>
        public ReservationEntity GetReservation()
        {
            var dataEntity = new ReservationDll().LoadEntity(p => p.ID == ID);
            return new ReservationEntity().GetViewModel(dataEntity);
        }

        /// <summary>
        /// 添加预约表单及预约表单字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddReservation(string name, List<ReservationContentEntity> viewFormRows)
        {
            var viewEntity = new ReservationEntity() { Name = name };
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //添加预约表单
                    var dataEntity = viewEntity.GetDataEntity(viewEntity);
                    int id = new ReservationDll().AddReservation(dataEntity);
                    if (id > 0)
                    {
                        //添加预约表单字段
                        viewFormRows.ForEach(p => p.ReservationID = id);
                        List<ReservationContent> dataFormRow = viewFormRows.Select(p => p.GetDataEntity(p)).ToList();
                        if (new ReservationContentDll().AddReservationContent(dataFormRow))
                        {
                            //如果添加预约表单及预约表单字段全部成功，提交事务
                            transactionScope.Complete();
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }
    }
}