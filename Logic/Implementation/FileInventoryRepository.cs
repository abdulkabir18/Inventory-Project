using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class FileInventoryRepository : IWriteInventoryRepository, IReadInventoryRepository
    {
        public static string productFileName = @"File\Product.txt";
        public static string saleFileName = @"File\Sale.txt";

        public FileInventoryRepository()
        {
            if (!File.Exists(productFileName))
            {
                File.Create(productFileName);
            }

            if (!File.Exists(saleFileName))
            {
                File.Create(saleFileName);
            }
        }

        public void SaveProduct(string productObject)
        {
            File.AppendAllText(productFileName, productObject + Environment.NewLine);
        }

        public void SaveSale(string saleObject)
        {
            File.AppendAllText(saleFileName, saleObject + Environment.NewLine);
        }

        public List<Product> LoadAllProducts()
        {
            var productResponse = File.ReadAllLines(productFileName)
            .Select(InventoryManager.ToProduct)
            .ToList();

            return productResponse;
        }

        public List<Sale> LoadAllSales()
        {
            var saleResponse = File.ReadAllLines(saleFileName)
            .Select(InventoryManager.ToSale)
            .ToList();
            return saleResponse;
        }
    }
}