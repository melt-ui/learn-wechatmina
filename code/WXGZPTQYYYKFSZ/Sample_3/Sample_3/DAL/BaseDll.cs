using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using Sample_3.ORM;

namespace Sample_3
{
    /// <summary>
    /// 仓储基类，用于数据访问
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDll<T> where T : EntityObject
    {
        //EF上下文
        public virtual ObjectContext DbContext
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }

        #region 新增实体

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSaveChage"> 是否保存改变</param>
        /// <returns></returns>
        public T AddEntity(T entity, bool isSaveChage = true)
        {
            if (DbContext.IsAttached(entity))
            {
                DbContext.CreateObjectSet<T>().Detach(entity);
            }
            DbContext.CreateObjectSet<T>().Attach(entity);
            DbContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);

            if (isSaveChage)
            {
                DbContext.SaveChanges();
            }
            return entity;
        }

        #endregion

        #region 修改实体

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSaveChage">是否保存改变 </param>
        /// <returns></returns>
        public bool UpdateEntity(T entity, bool isSaveChage = true)
        {
            if (DbContext.IsAttached(entity))
            {
                DbContext.CreateObjectSet<T>().Detach(entity);
            }
            DbContext.CreateObjectSet<T>().Attach(entity);
            DbContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
            if (isSaveChage)
            {
                return DbContext.SaveChanges() > 0;
            }
            return true;
        }
        #endregion

        #region 删除实体
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSaveChange">是否保存改变 </param>
        /// <returns></returns>
        public bool DeleteEntity(T entity, bool isSaveChange = true)
        {
            if (DbContext.IsAttached(entity))
            {
                DbContext.CreateObjectSet<T>().Detach(entity);
            }
            DbContext.CreateObjectSet<T>().Attach(entity);
            DbContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
            if (isSaveChange)
            {
                return DbContext.SaveChanges() > 0;
            }
            return true;
        }

        #endregion

        #region 保存改变
        /// <summary>
        /// 保存改变
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
        #endregion

        #region 查询单个实体
        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T LoadEntity(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> entitys = LoadEntities(expression);
            T entity = entitys.FirstOrDefault();
            if (entity != null)
            {
                if (DbContext.IsAttached(entity))
                    DbContext.Detach(entity);
            }
            return entity;
        }
        #endregion

        #region 查询实体
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> expression)
        {
            return DbContext.CreateObjectSet<T>().Where<T>(expression).AsQueryable();
        }

        #endregion

    }
}