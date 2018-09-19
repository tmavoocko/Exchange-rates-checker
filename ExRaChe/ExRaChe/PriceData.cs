using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExRaChe
{
    public class PriceData
    {
        public DateTime Date;
        public decimal Price;
        public PriceData(DateTime date, decimal price)
        {
            Date = date;
            Price = price;
        }

        public override string ToString()
        {
            return Date.ToShortDateString() + ": " + Price.ToString();
        }
    }
}
