using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace Sample_3
{
    /// <summary>
    /// 数据实体与视图实体的转换
    /// </summary>
    /// <typeparam name="TV">数据实体对应的视图实体类型</typeparam>
    /// <typeparam name="TD">数据实体类型</typeparam>
    public interface IViewModel<TV, TD> where TD : EntityObject
    {
        /// <summary>
        /// 数据实体转换为视图实体
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>视图实体</returns>
        TV GetViewModel(TD entity);
        /// <summary>
        /// 视图实体转换为数据实体
        /// </summary>
        /// <param name="entity">视图实体</param>
        /// <returns>数据实体</returns>
        TD GetDataEntity(TV entity);
    }
}