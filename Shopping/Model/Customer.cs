using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shopping.Model
{
    public class Customer : Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Good> GoodList = new List<Good>();

        public Guid Id { get; private set; }
        public Customer(string name, string phone = null, string email = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Phone = phone;
            Email = email;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nName: {Name}\nPhone: {Phone}\nEmail: {Email}\n";
        }

    }
}
