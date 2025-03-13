using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    public class User
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public UserRole Role { get; set; }
    }
}