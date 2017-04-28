using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample_5.ORM;

namespace Sample_5.DAL
{
    /// <summary>
    /// 推广渠道表的数据访问类
    /// </summary>
    public class ChannelDll : BaseDll<Channel>
    {
        /// <summary>
        /// 是否存在该渠道名称
        /// </summary>
        /// <param name="channelName">渠道名称</param>
        /// <param name="id">渠道ID</param>
        /// <returns></returns>
        public bool IsExitChannelName(string channelName, int id)
        {
            var entities = LoadEntities(p=>p.Name == channelName);
            if (id > 0)
            {
                entities = entities.Where(e => e.ID != id);
            }
            return entities.Any();
        }
    }
}