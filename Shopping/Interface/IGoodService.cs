using Shopping.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Interface
{
    public interface IGoodService
    {
        /// <summary>
        /// 查询所有商品
        /// </summary>
        void QueryAllGoods();

        /// <summary>
        /// 为顾客随机获取一些商品
        /// </summary>
        /// <param name="customer">顾客</param>
        /// <param name="count">商品数量</param>
        void GetSomeGoods(Customer customer, int count);

        /// <summary>
        /// 获取顾客购物车商品信息
        /// </summary>
        /// <param name="customer">顾客</param>
        void GetGoodInfo(Customer customer);

        /// <summary>
        /// 计算购物车总价格
        /// </summary>
        /// <param name="customer">顾客</param>
        /// <returns>总价格</returns>
        decimal GetTotalGoodsPrice(Customer customer);

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <returns>商品列表</returns>
        IEnumerable<Good> GetAllGoods();
    }
}
