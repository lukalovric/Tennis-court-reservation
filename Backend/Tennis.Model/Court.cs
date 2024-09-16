using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.Model
{
    public class Court
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsIndoor { get; set; }
    }
}
