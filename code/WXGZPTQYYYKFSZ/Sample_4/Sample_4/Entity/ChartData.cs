using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample_4
{
    /// <summary>
    /// HighChart统计图表数据
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// 1小时间隔秒数
        /// </summary>
        private const decimal HourPointInterval = 3600 * 1000;
        /// <summary>
        /// 图表起始年
        /// </summary>
        public decimal StartYear { get; set; }
        /// <summary>
        /// 图表起始月
        /// </summary>
        public decimal StartMonth { get; set; }
        /// <summary>
        /// 图表起始日
        /// </summary>
        public decimal StartDay { get; set; }
        /// <summary>
        /// 线间隔
        /// </summary>
        public decimal LineInterval
        {
            get
            {
                int pointCount = Statistics.Length;
                return (pointCount % 8 == 0 ? (pointCount / 8) : (pointCount / 8 + 1)) * 3600 * 1000;
            }
        }
        /// <summary>
        /// 点间隔
        /// </summary>
        public decimal PointInterval
        {
            get
            {
                return HourPointInterval;
            }
        }
        /// <summary>
        /// 点数据集合
        /// </summary>
        public decimal[] Statistics { get; set; }
    }
}