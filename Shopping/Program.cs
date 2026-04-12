using Shopping.Service;
using Shopping.Model;

namespace Shopping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceSystem serviceSystem = new ServiceSystem();
            serviceSystem.SysCustomerAuthService.Register("122654654", "cidaofjaldfja", "P1");

            try
            {
                Customer P1 = serviceSystem.SysCustomerAuthService.Login("122654654", "cidaofjaldfja");

                // serviceSystem.SysGoodService.QueryAllGoods();
                // serviceSystem.SysCustomerService.QueryAllCustomers();

                serviceSystem.SysGoodService.GetSomeGoods(P1, 20);
                serviceSystem.SysGoodService.GetGoodInfo(P1);
            }
            catch
            {
                throw new Exception("Login failed, user does not exist or password is incorrect.");
            }

        }
    }
}
