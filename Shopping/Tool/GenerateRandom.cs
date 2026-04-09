using Shopping.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Tool
{
    internal class GenerateRandom
    {
        private readonly Random _random = new Random();

        private readonly List<string> _name = new List<string> { 
            "Apple", "Banana", "Orange", "Grape", "Watermelon", "Strawberry", "Mango", 
            "Pineapple", "Kiwi", "Milk", "Yogurt", "Cheese", "Butter", "Bread", "Toast", "Cake", 
            "Cookie", "Chocolate", "Candy", "Potato Chips", "Popcorn", "Coca Cola", "Sprite", 
            "Juice", "Mineral Water", "Coffee", "Tea", "Energy Drink", "Instant Noodles", "Sausage", 
            "Rice", "Flour", "Cooking Oil", "Salt", "Sugar", "Soy Sauce", "Vinegar", "Tissue Paper", 
            "Laundry Detergent", "Dish Soap", "Toothbrush", "Toothpaste", "Towel", "Shampoo", 
            "Body Wash", "Facial Cleanser", "Notebook", "Pen", "Pencil", "Folder", "Mouse", "Keyboard", 
            "Headphone", "Charger", "Data Cable", "Backpack", "Umbrella", "Slippers", "Socks", "Gloves" 
        };

        public List<Good> GenerateRandomGoods(int count)
        {
            List<Good> goods = new List<Good>();

            int id = 1;

            for (int i = 0; i < count; i++)
            {
                string goodName = _name[_random.Next(_name.Count)];
                decimal goodPrice = (decimal)_random.Next(10, 101);

                Good good = new Good
                {
                    Name = goodName
                };

                good.GoodInfo[id] = goodPrice;

                id++;

                goods.Add(good);

            }

            return goods;
        }
    }
}
