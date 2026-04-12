using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Model;

namespace Shopping.Repository
{
    public class CustomerRepository
    {
        private readonly List<Customer> _customerList = new List<Customer>();
        public List<Customer> CustomerList => _customerList;

    }
}
