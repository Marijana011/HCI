using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content_Management_System.Classes
{
    public enum UserRole{ Visitor, Admin}
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public UserRole Role { get; set; }

        
    }
}
