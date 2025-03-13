using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public interface IReadInventoryRepository
    {
        List<Product> LoadAllProducts();
        List<Sale> LoadAllSales();
    }
}