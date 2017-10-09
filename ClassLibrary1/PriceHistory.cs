using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class PriceHistory
    {
        private Price price;

        public PriceHistory(Price price)
        {
            Price = price;
        }

        public Price Price { get => price; set => price = value; }
    }
}
