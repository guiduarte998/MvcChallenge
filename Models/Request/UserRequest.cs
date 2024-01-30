using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
