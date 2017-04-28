using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Sample_3.ORM;

namespace Sample_3
{
    public class UserReservationBll
    {
        /// <summary>
        /// 添加用户填写的预约表单及预约表单字段内容
        /// </summary>
        /// <param name="openId">微信OPENID</param>
        /// <param name="reservationID">预约ID</param>
        /// <returns></returns>
        public bool AddUserReservation(string openId, int reservationID, List<UserReservationContentEntity> viewFormRows)
        {
            var viewEntity = new UserReservationEntity() { ReservationID = reservationID, WeixinOpenId = openId };
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    //添加用户填写的预约表单
                    var dataEntity = viewEntity.GetDataEntity(viewEntity);
                    int id = new UserReservationDll().AddEntity(dataEntity).ID;
                    if (id > 0)
                    {
                        //添加用户填写的预约表单字段内容
                        viewFormRows.ForEach(p => p.UserReservationId = id);
                        List<UserReservationContent> dataFormRow = viewFormRows.Select(p => p.GetDataEntity(p)).ToList();
                        if (new UserReservationContentDll().AddUserReservationContent(dataFormRow))
                        {
                            //如果添加用户填写的预约表单及预约表单字段内容全部成功，提交事务
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