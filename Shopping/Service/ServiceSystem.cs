using Microsoft.AspNetCore.Http;
using Shopping.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Service
{
    public class ServiceSystem
    {
        private CustomerService _sysCustomerService = new CustomerService();
        private IGoodService _sysGoodService = new GoodService();
        public IGoodService SysGoodService => _sysGoodService;
        public CustomerService SysCustomerService => _sysCustomerService;

        private CustomerAuthService _sysCustomerAuthService = new CustomerAuthService();
        public CustomerAuthService SysCustomerAuthService => _sysCustomerAuthService;
    }
}
