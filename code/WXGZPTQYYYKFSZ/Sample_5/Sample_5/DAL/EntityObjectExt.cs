using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace Sample_5
{
    public static class EntityObjectExt
    {
        /// <summary>
        /// 判断传入的对象是否已附加到当前实体数据对象中
        /// </summary>
        /// <param name="context">实体数据对象</param>
        /// <param name="entity">对象</param>
        /// <returns>是否附加成功</returns>
        public static bool IsAttached(this ObjectContext context, object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }
            try
            {
                var dd = entity as EntityObject;
                ObjectStateEntry entry = context.ObjectStateManager.GetObjectStateEntry(dd.EntityKey);
                return (entry.State != EntityState.Detached);
            }
            catch
            {
                return false;
            }
        }
    }
}