using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Model;
using Shopping.Tool;

namespace Shopping.Repository
{
    public class GoodRepository
    {
        private readonly GenerateRandom _goodGenerator = new GenerateRandom();
        private readonly List<Good> _myShopGood;

        public List<Good> MyShopGood {  get { return _myShopGood; } }

        public GoodRepository(int count)
        {
            _myShopGood = _goodGenerator.GenerateRandomGoods(count);
        }
    }
}
