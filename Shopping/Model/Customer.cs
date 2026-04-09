using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Model
{
    public class Customer : Person
    {
        public List<Good> GoodList = new List<Good>();

        public override string ToString()
        {
            return base.ToString();
        }

        public void GetGoodInfo()
        {
            if(GoodList == null || GoodList.Count == 0)
            {
                Console.WriteLine($"顾客{base.Name} 没有买东西"); return;
            }

            // map: 商品名称 → (单价 → 数量)
            Dictionary<string, Dictionary<decimal, int>> map = new Dictionary<string, Dictionary<decimal, int>>();

            Dictionary<decimal, int> goodTmp = new Dictionary<decimal, int>();

            foreach (Good item in GoodList)
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


                if(!priceDict.ContainsKey(goodPrice))
                {
                    priceDict[goodPrice] = 1;
                }
                else
                {
                    priceDict[goodPrice] += 1;
                }
               
            }

            Console.WriteLine($"============== 客户: {base.Name} ==================================");
            Console.WriteLine("==================== 商品信息 ======================================");
            foreach(var good in map)
            {
                var priceDict = good.Value;
                if (priceDict.Count == 0) continue;

                decimal price = priceDict.Keys.First();
                int quantity = priceDict.Values.First();

                decimal totalPrice = price * quantity;

                Console.WriteLine($"名称：{good.Key,-20} 单价：{price, -3} ￥         x {quantity, -6}  |  总价： {totalPrice, 10:C}");

            }

            Console.WriteLine("====================================================================");
        }



    }
}
