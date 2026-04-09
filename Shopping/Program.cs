using Shopping.Service;
using Shopping.Model;





namespace Shopping
{
    internal class Program
    {
        static void Main(string[] args)
        {

            GoodService service = new GoodService();
            Customer P1 = new Customer();
            P1.Name = "Mike";
            Customer P2 = new Customer();
            P2.Name = "John";

            /// 查询超市中有多少商品
            Console.WriteLine("超市中含有的商品如下")
;            service.QueryAllGoods();

            Console.WriteLine("\n顾客1购买的商品如下");
            /// 从超市中购买一些物品
            P1.GoodList = service.GetSomeGoods(50);
            Console.WriteLine("\n顾客1购买的商品总额");
            service.GetGoodInfo(P1);

        }
    }
}
