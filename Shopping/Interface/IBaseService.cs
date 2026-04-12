using Shopping.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Interface
{
    public interface IBaseService<T, TId> where T : class
    {
        /// <summary>
        /// 查询所有实体
        /// </summary>
        void QueryAll();

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>实体对象</returns>
        T GetById(TId id);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        void Add(T entity);

        /// <summary>
        /// 获取所有实体列表
        /// </summary>
        /// <returns>实体列表</returns>
        IEnumerable<T> GetAll();
    }
}
