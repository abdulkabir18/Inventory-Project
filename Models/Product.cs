using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Name}, {Price}, {StockQuantity}, {Category}";
        }
    }
}