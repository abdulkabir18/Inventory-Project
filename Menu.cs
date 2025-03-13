using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystem;




namespace InventoryManagementSystem
{
    public class Menu
    {
        InventoryManager inventoryManager = new InventoryManager();
        Product product = new Product();
        Sale sale = new Sale();
        public Menu()
        {
            while (true)
            {
                Console.WriteLine(@"
        Press 1 To Add Product.
        Press 2 To Update Product.
        Press 3 To Delect Product.
        Press 4 To Veiw All Products.
        Press 5 To Record Sale.
        Press 6 To View Low Stock Products.
        Press 7 To View Report.
        Press 8 To Logout.");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        int productID = inventoryManager.GetProducts().Count + 1;
                        bool valid = inventoryManager.IDIsValid(productID);

                        if (valid)
                        {
                            product.ID = productID++;
                        }
                        else
                        {
                            product.ID = ++productID;
                        }


                        Console.Write("Enter the Product Name: ");
                        string name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.Write("\nInvalid Input (NAME)!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        product.Name = name;

                        Console.Write("Enter the Product Price ($): ");
                        var priceInput = Console.ReadLine();

                        if (!double.TryParse(priceInput, out double price))
                        {
                            Console.Write("\nInvalid Input (PRICE)!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        product.Price = price;

                        Console.Write("Enter the Product Quantity: ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.Write("\nInvalid Input (QUANTITY)!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        product.StockQuantity = quantity;

                        Console.Write("Enter the Product Category: ");
                        string category = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(category))
                        {
                            Console.WriteLine("\nInvalid Input (CATEGORY)!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        product.Category = category;

                        inventoryManager.AddProduct(product);
                        break;
                    case "2":
                        if (inventoryManager.GetProducts().Count > 0)
                        {
                            Console.Write("Enter ID/Name Of The Product You Want To Update: ");
                            string update = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(update))
                            {
                                Console.Write("\nInvalid Input!\nPress Enter To Continue: ");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                inventoryManager.UpdateProductRecord(update);
                            }
                            break;
                        }
                        else
                            Console.Write("\nNo Record To Update\nPress Enter To Continue: ");
                        Console.ReadLine();
                        break;
                    case "3":
                        if (inventoryManager.GetProducts().Count > 0)
                        {
                            Console.Write("Enter ID/Name Of The Product You Want To Delect: ");
                            string delect = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(delect))
                            {
                                Console.Write("\nInvalid Input!\nPress Enter To Continue: ");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                inventoryManager.DelectProduct(delect);
                            }
                            break;
                        }
                        else
                            Console.Write("\nNo Record To Delect\nPress Enter To Continue: ");
                        Console.ReadLine();
                        break;
                    case "4":
                        var products = inventoryManager.GetProducts();
                        if (products.Count != 0)
                        {
                            foreach (Product product in products)
                            {
                                Console.WriteLine(@$"ID: {product.ID}
                        Name: {product.Name}
                        Price: {product.Price:c}
                        Quantity: {product.StockQuantity}
                        Category: {product.Category}
                        ");
                            }
                            Console.Write("Press Enter To Continue: ");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("\nNo Product Has Been Recorded!\nPress Enter To Continue: ");
                            Console.ReadLine();
                        }

                        break;
                    case "5":
                        int saleID = inventoryManager.GetSales().Count + 1;
                        sale.ID = saleID++;

                        Console.Write("Enter The Product ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int productId))
                        {
                            Console.Write("\nInvalid Input (ID)!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        sale.ProductID = productId;

                        Console.Write("Enter the Sale Quantity: ");
                        if (!int.TryParse(Console.ReadLine(), out int salequantity))
                        {
                            Console.Write("\nInvalid Input! (QUANTITY)\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        sale.Quantity = salequantity;

                        sale.SaleDate = DateTime.Now;

                        bool isValid = inventoryManager.ValidSaleDetails(sale);
                        if (isValid == false)
                        {
                            break;
                        }

                        inventoryManager.UpdateQuntity(salequantity, productId);
                        inventoryManager.AddSale(sale);
                        Console.Write("Sale Recorded Successfully!\nPress Enter To Continue: ");
                        Console.ReadLine();
                        break;
                    case "6":
                        var lowStock = inventoryManager.GetLowStockProducts();
                        if (lowStock.Count == 0)
                        {
                            Console.Write("\nNo Low Stock Found!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        foreach (var stock in lowStock)
                        {
                            Console.WriteLine($"ID: {stock.ID}\nName: {stock.Name}\nPrice: {stock.Price:c}\nStock Quantity: {stock.StockQuantity}\nCategory: {stock.Category}\n");
                        }
                        Console.Write("Press Enter To Continue: ");
                        Console.ReadLine();
                        break;
                    case "7":
                        var reports = inventoryManager.GetSales();
                        if (reports.Count == 0)
                        {
                            Console.Write("\nNo Sale Record Found!\nPress Enter To Continue: ");
                            Console.ReadLine();
                            break;
                        }
                        foreach (var report in reports)
                        {
                            Console.WriteLine($"Sale ID: {report.ID}\nProduct ID: {report.ProductID}\nQuantity: {report.Quantity}\nSale Date: {report.SaleDate}\n");
                        }
                        Console.Write("Press Enter To Continue: ");
                        Console.ReadLine();
                        break;
                    case "8":
                        return;
                    default:
                        Console.Write("\nInvalid Input!\nPress Enter To Continue: ");
                        Console.ReadLine();
                        break;
                }
            }
        }




    }
}