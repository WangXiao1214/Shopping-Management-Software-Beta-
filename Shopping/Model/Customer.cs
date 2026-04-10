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

    }
}
