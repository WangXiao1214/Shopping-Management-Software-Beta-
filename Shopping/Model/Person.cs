using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Model
{
    public class Person
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} | Name: {Name} | Age: {Age} | Birthday: {Birthday:yyyy-MM-dd}";
        }

    }

    public enum Gender
    {
        Male,
        Female
    }


}
