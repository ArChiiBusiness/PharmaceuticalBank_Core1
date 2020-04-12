using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL
{
    public partial class AspNetRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
