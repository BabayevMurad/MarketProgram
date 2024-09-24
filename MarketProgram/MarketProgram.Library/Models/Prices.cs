using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProgram.Library.Models
{
    public class Prices
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public double Price { get; set; }

        public Prices(DateTime dateTime, double price)
        {
            DateTime = dateTime;
            Price = price;
        }

        public override string ToString()
        {
            return $"Date: {DateTime}; Price: {Price}";
        }
    }
}
