using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class InventoryManager
    {
        private static List<User> users = new List<User>();

        public static void GetData()
        {
            Data();
        }
        static void Data()
        {
            users.Add(new User
            {
                UserName = "admin@clh",
                PassWord = "admin",
                Role = UserRole.Admin
            });

            users.Add(new User
            {
                UserName = "manager@clh",
                PassWord = "manager",
                Role = UserRole.Manager
            });

            users.Add(new User
            {
                UserName = "staff1@clh",
                PassWord = "staff1",
                Role = UserRole.Staff
            });

            users.Add(new User
            {
                UserName = "staff2@clh",
                PassWord = "staff2",
                Role = UserRole.Staff
            });
        }

        public static User? Authentication(string username, string password)
        {
            foreach (User user in users)
            {
                if (user.UserName == username && user.PassWord == password)
                {
                    return user;
                }
            }
            return null;
        }

        private readonly IReadInventoryRepository _readInventoryRepository;
        private readonly IWriteInventoryRepository _writeInventoryRepository;
        // Accessing the data base 
        public InventoryManager()
        {
            _readInventoryRepository = new FileInventoryRepository();
            _writeInventoryRepository = new FileInventoryRepository();
        }

        // Getting the product object back from string format
        public static Product ToProduct(string data)
        {
            var record = data.Split(", ");
            return new Product()
            {
                ID = int.Parse(record[0]),
                Name = record[1],
                Price = double.Parse(record[2]),
                StockQuantity = int.Parse(record[3]),
                Category = record[4]
            };
        }

        // Getting the sale object back from string format
        public static Sale ToSale(string data)
        {
            var record = data.Split(", ");
            return new Sale()
            {
                ID = int.Parse(record[0]),
                ProductID = int.Parse(record[1]),
                Quantity = int.Parse(record[2]),
                SaleDate = DateTime.Parse(record[3])
            };
        }

        // Validate ID
        public bool IDIsValid(int nextID)
        {
            var products = GetProducts();
            if(products.Count == 0)
            {
                return true;
            }
            foreach(var product in products)
            {
                if(product.ID == nextID)
                {
                    return false;
                }
            }
            return true;
        }

        // Add Product
        public void AddProduct(Product product)
        {
            List<Product> products = _readInventoryRepository.LoadAllProducts();
            foreach (var item in products)
            {
                bool flag = item.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase);
                if (flag)
                {
                    Console.Write("\nThis Product Name Is Already Added To The Stock\nPress Enter To Continue: ");
                    Console.ReadLine();
                    return;
                }
            }

            var _product = product.ToString();
            _writeInventoryRepository.SaveProduct(_product);
            Console.Write("\nProduct Added Successfully!\nPress Enter To Continue: ");
            Console.ReadLine();
        }

        // Update product
        public void UpdateProductRecord(string respond)
        {
            List<Product> products = _readInventoryRepository.LoadAllProducts();

            Product updatedProduct = null;
            foreach (var product in products)
            {
                // Comparing the values Not Base on Casing
                bool flag = respond.Equals(product.Name, StringComparison.OrdinalIgnoreCase);
                int.TryParse(respond, out int id);
                if (product != null && flag || product.ID == id)
                {
                    Console.WriteLine("Update Your Product Record\n");
                    updatedProduct = product;

                    Console.Write("New Price (leave blank to keep current Price)$: ");
                    var priceInput = Console.ReadLine();

                    if (double.TryParse(priceInput, out double price))
                    {
                        product.Price = price;
                    }

                    Console.Write("New Stock Quantity (leave black to keep current Quantity): ");
                    var quantityInput = Console.ReadLine();

                    if (int.TryParse(quantityInput, out int stockQuantity))
                    {
                        product.StockQuantity = stockQuantity;
                    }

                    OverwriteProductFlie(products);
                    Console.Write("\nProduct Updated Successfully!\nPress Enter To Continue: ");
                    Console.ReadLine();
                    return;
                }
            }

            if (updatedProduct == null)
            {
                Console.Write("\nProduct not found!\nPress Enter To Continue: ");
                Console.ReadLine();
                return;
            }
        }

        // Delect Product
        public void DelectProduct(string respond)
        {
            List<Product> products = _readInventoryRepository.LoadAllProducts();

            foreach (var product in products)
            {
                bool flag = respond.Equals(product.Name, StringComparison.OrdinalIgnoreCase);
                int.TryParse(respond, out int id);

                if (product != null && flag || product.ID == id)
                {
                    products.Remove(product);
                    OverwriteProductFlie(products);
                    Console.Write("\nProduct deleted successfully!\nPress Enter To Continue: ");
                    Console.ReadLine();
                    return;
                }

            }
            Console.Write("\nProduct not Found!\nPress Enter To Continue: ");
            Console.ReadLine();
        }

        // View Products
        public List<Product> GetProducts()
        {
            return _readInventoryRepository.LoadAllProducts();
        }

        // View Low Stock Products
        public List<Product> GetLowStockProducts()
        {
            // return GetProducts().Where(x => x.StockQuantity <= 20).ToList();
            // var products = GetProducts();

            var lowStockProducts = new List<Product>();

            foreach (var product in GetProducts())
            {
                if (product.StockQuantity <= 20)
                {
                    lowStockProducts.Add(product);
                }
            }
            return lowStockProducts;
        }

        // RecordSale

        //////////// Checking the sale input against the list of product
        public bool ValidSaleDetails(Sale sale)
        {
            List<Product> productList = _readInventoryRepository.LoadAllProducts();
            if (productList.Count > 0)
            {
                foreach (var product in productList)
                {
                    if (product.ID == sale.ProductID)
                    {
                        if (product.StockQuantity >= sale.Quantity)
                        {
                            return true;
                        }
                        else if (product.StockQuantity == 0)
                        {
                            Console.Write($"\nProduct: {product.Name} Not Available In Stock!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            productList.Remove(product);
                            OverwriteProductFlie(productList);
                            return false;
                        }
                        Console.Write("\nThe Quantity Entered Is Greater Than The Stock Quantity!\nPress Enter To Continue: ");
                        Console.ReadLine();
                        return false;
                    }
                }
                Console.Write("\nThe Product ID is Not Found!\nPress Enter To Continue: ");
                Console.ReadLine();
                return false;
            }
            Console.Write("\nNo Product Has Been Added To The Stock!\nPress Enter To Continue: ");
            Console.ReadLine();
            return false;
        }

        //////////// Getting and saving back to the file 
        public void OverwriteProductFlie(List<Product> products)
        {
            string path = FileInventoryRepository.productFileName;
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                foreach (var item in products)
                {
                    streamWriter.WriteLine(item);
                }
            }
        }

        //////////// Updating the quantity of sale with the product stock quantity
        public void UpdateQuntity(int saleQuantity, int productID)
        {
            List<Product> products = _readInventoryRepository.LoadAllProducts();

            foreach (var product in products)
            {
                if (product.ID == productID)
                {
                    product.StockQuantity -= saleQuantity;
                    OverwriteProductFlie(products);
                    if (product.StockQuantity == 0)
                    {
                        Console.Write($"\nProduct: {product.Name} Is No Longer Available In Stock\nPress Enter To Continue: ");
                        Console.ReadLine();
                        products.Remove(product);
                        OverwriteProductFlie(products);
                        return;
                    }
                    Console.WriteLine($"\nProduct: {product.Name} Available Stock Quantity {product.StockQuantity}");
                    return;
                }
            }
        }

        // Adding the sale record to the file
        public void AddSale(Sale sale)
        {
            sale.SaleDate = DateTime.UtcNow;
            var _sale = sale.ToString();
            _writeInventoryRepository.SaveSale(_sale);
        }

        // Generate Report
        public List<Sale> GetSales()
        {
            return _readInventoryRepository.LoadAllSales();
        }
    }
}