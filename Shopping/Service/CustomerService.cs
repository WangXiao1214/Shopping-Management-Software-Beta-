using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Repository;
using Shopping.Model;

namespace Shopping.Service
{
    public class CustomerService
    {
        private static readonly CustomerRepository _customerRepository = new CustomerRepository();


        public void QueryAllCustomers()
        {
            foreach(var customer in _customerRepository.CustomerList)
            {
                Console.WriteLine(customer);
            }
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.CustomerList.Add(customer);
        }

        public Customer GetCustomerById(Guid id)
        {
            return  _customerRepository.CustomerList.Find(c => c.Id == id);
        }
    }
}
