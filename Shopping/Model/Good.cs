using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.Model
{
    public class Good
    {
        public Dictionary<int, decimal> GoodInfo = new Dictionary<int, decimal>();
        public string? Name { get; set; }

        public override string ToString()
        {
            if (GoodInfo.Count == 0)
                return $"Name: {Name,-30} | ID: {"N/A",-6} | Price: {"N/A",8}";

            int id = GoodInfo.Keys.First();
            decimal price = GoodInfo.Values.First();

            return $"ID: {id,-3} | Name: {Name,-15} | Price: {price,3}";
        }
    }
}