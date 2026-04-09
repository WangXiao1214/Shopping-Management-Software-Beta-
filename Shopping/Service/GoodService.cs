using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Repository;
using Shopping.Model;

namespace Shopping.Service
{
    public class GoodService
    {
        private readonly Random _randomGetGoods = new Random();
        private readonly GoodRepository myShopRepos = new GoodRepository(100);

        public void QueryAllGoods()
        {

            foreach(var good in myShopRepos.MyShopGood)
            {
                Console.WriteLine(good);
            }

        }


        /// <summary>
        /// 从 超市系统中 获取一些物品订单
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Good> GetSomeGoods(int count)
        {
            
            var result = new List<Good>();

            for (int i = 0; i<=count; i++)
            {
                var randomId = _randomGetGoods.Next(myShopRepos.MyShopGood.Count);
                result.Add(myShopRepos.MyShopGood[randomId]);
            }
            
            return result;

        }

        /// <summary>
        /// 统计客户购买的物品数目以及总价格
        /// </summary>
        /// <param name="customer"></param>
        public void GetGoodInfo(Customer customer)
        {
            if (customer.GoodList == null || customer.GoodList.Count == 0)
            {
                Console.WriteLine($"顾客{customer.Name} 没有买东西"); return;
            }

            // map: 商品名称 → (单价 → 数量)
            Dictionary<string, Dictionary<decimal, int>> map = new Dictionary<string, Dictionary<decimal, int>>();

            Dictionary<decimal, int> goodTmp = new Dictionary<decimal, int>();

            foreach (Good item in customer.GoodList)
            {
                string goodName = item.Name;



                if (item.GoodInfo == null || item.GoodInfo.Count == 0)
                {
                    Console.WriteLine($"警告：商品 {goodName} 价格信息为空");
                    return;
                }

                decimal goodPrice = item.GoodInfo.Values.First(); // 只有一个价格

                // === 关键：确保外层字典有这个商品 ===
                if (!map.ContainsKey(goodName))
                {
                    map[goodName] = new Dictionary<decimal, int>();
                }

                var priceDict = map[goodName];


                if (!priceDict.ContainsKey(goodPrice))
                {
                    priceDict[goodPrice] = 1;
                }
                else
                {
                    priceDict[goodPrice] += 1;
                }

            }

            Console.WriteLine($"============== 客户: {customer.Name} ==================================");
            Console.WriteLine("==================== 商品信息 ======================================");
            foreach (var good in map)
            {
                var priceDict = good.Value;
                if (priceDict.Count == 0) continue;

                decimal price = priceDict.Keys.First();
                int quantity = priceDict.Values.First();

                decimal totalPrice = price * quantity;

                Console.WriteLine($"名称：{good.Key,-20} 单价：{price,-3} ￥         x {quantity,-6}  |  总价： {totalPrice,10:C}");

            }

            Console.WriteLine($"=========================== 总价格为 {GetTotalGoodsPrice(customer),5:C} ==================================");
        }


        /// <summary>
        /// 计算购物车的总价格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cart"></param>
        public decimal GetTotalGoodsPrice(Customer customer)
        {

            decimal totalPrice = 0m;

            foreach(var good in customer.GoodList)
            {
                decimal price = good.GoodInfo.Values.First();

                totalPrice += price;
            }

            return totalPrice;

        }

    }
}
