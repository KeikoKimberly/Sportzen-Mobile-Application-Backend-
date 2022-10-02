using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportzen.API.Model.Request
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
