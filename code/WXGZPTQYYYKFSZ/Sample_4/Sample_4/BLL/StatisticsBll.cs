using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample_4
{
    public class StatisticsBll
    {
        /// <summary>
        /// 访问与分享记录的内存数据库
        /// </summary>
        private static Statistics _statistics;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static StatisticsBll()
        {
            //初始化内存数据库
            _statistics = new Statistics();
            _statistics.PageNav = new List<PageNavEntity>();
            _statistics.PageShare = new List<PageShareEntity>();
        }

        /// <summary>
        /// 添加访问记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void InsertPageNav(PageNavEntity entity)
        {
            entity.VisitTime = DateTime.Now;
            _statistics.PageNav.Add(entity);
        }

        /// <summary>
        /// 添加分享记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void InsertPageShare(PageShareEntity entity)
        {
            entity.ShareTime = DateTime.Now;
            _statistics.PageShare.Add(entity);
        }

        /// <summary>
        /// 获取访问记录
        /// </summary>
        /// <returns></returns>
        public static List<PageNavEntity> GetPageNavList()
        {
            return _statistics.PageNav;
        }

        /// <summary>
        /// 获取分享记录
        /// </summary>
        /// <returns></returns>
        public static List<PageShareEntity> GetPageShareList()
        {
            return _statistics.PageShare;
        }
    }
}