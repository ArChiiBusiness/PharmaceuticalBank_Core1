using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaceuticalBank_Core1.Models
{
    public class ShipmentViewModel
    {
        public System.Guid Id { get; set; }
        public string GoodsShipped { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public CompanyViewModel Buyer { get; set; }
        public CompanyViewModel Seller { get; set; }

    }
}
