using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaceuticalBank_Core1.Models
{
    public class CompanyViewModel
    {
        public System.Guid Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String Country { get; set; }

    }
}
