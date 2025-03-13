using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public interface IWriteInventoryRepository
    {
        void SaveProduct(string productObject);
        void SaveSale(string saleObject);
    }
}