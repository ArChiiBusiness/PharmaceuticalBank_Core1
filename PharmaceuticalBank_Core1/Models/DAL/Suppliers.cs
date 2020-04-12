using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL
{
    public partial class Suppliers
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string SupplierName { get; set; }
    }
}
