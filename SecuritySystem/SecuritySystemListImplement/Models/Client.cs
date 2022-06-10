using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemListImplement.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientFLM { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
