using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaceuticalBank_Core1.Models
{
    public class SearchResultViewModel
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public Guid ShipmentId { get; set; }

    }
}
