using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Tennis.Model
{
    public class User
    {   
        public Guid? Id { get; set; }
        public string? FullName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }
        public Boolean? IsAdmin { get; set; }
        public string? Token { get; set; }
    }
}