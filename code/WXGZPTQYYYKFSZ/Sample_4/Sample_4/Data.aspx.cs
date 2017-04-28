using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Sample_4
{
    /// <summary>
    /// 为页面HighCharts画图控件提供数据
    /// </summary>
    public partial class Data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = "";
            string typeStr = System.Web.HttpContext.Current.Request.QueryString["type"];
            if (!string.IsNullOrEmpty(typeStr))
            {
                switch (typeStr)
                {
                    case "navChart": //页面访问图
                        result = JsonConvert.SerializeObject(GetPageNavStatistics());
                        break;
                    case "shareChart": //页面分享图
                        result = JsonConvert.SerializeObject(GetPageShareStatistics());
                        break;
                }
            }
            //将HighCharts绘图所需的数据返回给页面
            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ContentType = "application/json";
            response.Write(result);
            response.End();
        }

        /// <summary>
        /// 获取页面访问统计信息
        /// </summary>
        /// <returns></returns>
        private ChartData GetPageNavStatistics()
        {
            //取过去两天的数据进行统计
            DateTime startTime = DateTime.Now.AddDays(-3);
            DateTime endTime = DateTime.Now.AddDays(1);
            List<PageNavEntity> temp = StatisticsBll.GetPageNavList();
            List<decimal> statistics = new List<decimal>();
            //HighCharts时间轴的起始时间
            ChartData chartData = new ChartData
            {
                StartYear = startTime.Year,
                StartDay = startTime.Day,
                StartMonth = startTime.Month
            };
            //生成按小时统计的数据
            while (startTime < endTime)
            {
                statistics.Add(temp.FindAll(e => e.VisitTime >= startTime && e.VisitTime < startTime.AddHours(1)).Count());
                startTime = startTime.AddHours(1);
            }
            chartData.Statistics = statistics.ToArray();
            return chartData;
        }

        /// <summary>
        /// 获取页面分享统计信息
        /// </summary>
        /// <returns></returns>
        private ChartData GetPageShareStatistics()
        {	    
            //取过去两天的数据进行统计
            DateTime startTime = DateTime.Now.AddDays(-3);
            DateTime endTime = DateTime.Now.AddDays(1);
            List<PageShareEntity> temp = StatisticsBll.GetPageShareList();
            List<decimal> statistics = new List<decimal>();
            //HighCharts时间轴的起始时间
            ChartData chartData = new ChartData
            {
                StartYear = startTime.Year,
                StartDay = startTime.Day,
                StartMonth = startTime.Month
            };
            //生成按小时统计的数据
            while (startTime < endTime)
            {
                statistics.Add(temp.FindAll(e => e.ShareTime >= startTime && e.ShareTime < startTime.AddHours(1)).Count());
                startTime = startTime.AddHours(1);
            }
            chartData.Statistics = statistics.ToArray();
            return chartData;
        }
    }
}