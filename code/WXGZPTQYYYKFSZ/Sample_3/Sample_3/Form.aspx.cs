using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample_3
{
    public partial class Form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {                
                //表单POST提交操作
                //获取预约表单ID
                int reservationID;
                int.TryParse(Request.Form["reservationID"], out reservationID);
                if (reservationID > 0)
                {
                    //获取微信个人用户的OPENID
                    string openId = Request.Form["openId"];
                    if (!string.IsNullOrEmpty(openId))
                    {
                        //从POST提交的数据，构造用户填写的预约表单各字段的内容实体
                        List<UserReservationContentEntity> formRow = new List<UserReservationContentEntity>();
                        var reservationContents = ReservationContents(reservationID);
                        foreach (var reservationContent in reservationContents)
                        {
                            string content = Request.Form["content" + reservationContent.ID.ToString()];
                            formRow.Add(new UserReservationContentEntity() { Content = content, ReservationContentId = reservationContent.ID });
                        }
                        if (formRow.Count > 0)
                        {
                            //添加用户填写的预约表单及预约表单字段内容
                            if (new UserReservationBll().AddUserReservation(openId, reservationID, formRow))
                            {
                                Response.Write("保存成功");
                                Response.End();
                            }
                            else
                            {
                                Response.Write("保存失败");
                                Response.End();
                            }
                        }
                    }
                }
            }
            else
            {
                //页面初始化，获取前端页面需要的预约表单信息
                var reservation = Reservation();
                ViewState["Reservation"] = reservation;
                ViewState["ReservationContents"] = ReservationContents(reservation.ID);
            }
        }

        /// <summary>
        /// 获取预约实体
        /// </summary>
        /// <returns></returns>
        protected ReservationEntity Reservation()
        {
            return new ReservationBll().GetReservation();
        }

        /// <summary>
        /// 获取预约表单字段实体
        /// </summary>
        /// <param name="reservationID">预约ID</param>
        /// <returns></returns>
        protected List<ReservationContentEntity> ReservationContents(int reservationID)
        {
            return new ReservationContentBll().GetReservationContents(reservationID);
        }
    }
}