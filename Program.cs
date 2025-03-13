using InventoryManagementSystem;
internal class Program
{
    public static int count = 0;
    static void Main(string[] args)
    {
        InventoryManager.GetData();

        Console.WriteLine("WELCOME TO CLH INVENTORY MANAGEMENT SYSTEM");
        MainMenu();

        void MainMenu()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                count++;
                if (count > 3)
                {
                    Console.WriteLine("\nInvalid Input. Exiting program.....");
                    Environment.Exit(0);
                }
                Console.Write("\nInvalid Input (USERNAME)!\nPress Enter To Continue: ");
                Console.ReadLine();
                MainMenu();
            }

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                count++;
                if (count > 4)
                {
                    Console.WriteLine("\nInvalid Input. Exiting program.....");
                    Environment.Exit(0);
                }
                Console.Write("\nInvalid Input (PASSWORD)!\nPress Enter To Continue: ");
                Console.ReadLine();
                MainMenu();
            }

            User user = InventoryManager.Authentication(username, password);
            if (user == null)
            {
                count++;
                if (count >= 6)
                {
                    Console.WriteLine("Invalid credentiels. Exiting program.");
                    Environment.Exit(0);
                }

                Console.Write("Invalid Credential\nPress Enter To Continue: ");
                Console.ReadLine();
                MainMenu();
            }
            else
            {
                if ((user.Role == UserRole.Admin) || (user.Role == UserRole.Manager) || (user.Role == UserRole.Staff))
                {
                    Menu menu = new Menu();
                }
            }
        }
    }

}


