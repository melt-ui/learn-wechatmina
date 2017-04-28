using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sample_3
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                //表单POST提交操作
                int count;
                //获取表单行数
                int.TryParse(Request.Form["lineCount"], out count);
                //获取预约名称
                string reservationName = Request.Form["formName"];
                if (count > 0 && !string.IsNullOrEmpty(reservationName))
                {
                    //从POST提交的数据，构造预约表单字段实体
                    List<ReservationContentEntity> formRow = new List<ReservationContentEntity>(); 
                    for (int i = 0; i < count; ++i)
                    {
                        string name = Request.Form["Name" + i.ToString()];
                        string content = Request.Form["Content" + i.ToString()];
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(content))
                        {
                            formRow.Add(new ReservationContentEntity() { Name = name, Content = Request.Form["Content" + i.ToString()] });
                        }
                    }
                    if (formRow.Count > 0)
                    {
                        //添加预约表单及预约表单字段
                        if (new ReservationBll().AddReservation(reservationName, formRow))
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
    }
}