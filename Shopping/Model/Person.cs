using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Model
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }


}
