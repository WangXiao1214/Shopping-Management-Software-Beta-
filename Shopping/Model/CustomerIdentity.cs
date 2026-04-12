using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Model;


namespace Shopping.Model
{
    public class CustomerIdentity
    {
        public string Account { get; private set; }
        public string PasswordHash { get; private set; }
        public Guid CustomerId { get; private set; }

        public CustomerIdentity(string account, string passwordHash, Guid customerId)
        {
            Account = account;
            PasswordHash = passwordHash;
            CustomerId = customerId;
        }
    }
}
