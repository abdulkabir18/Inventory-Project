using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class Sale
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }

        public override string ToString()
        {
            return $"{ID}, {ProductID}, {Quantity}, {SaleDate}";
        }
    }
}