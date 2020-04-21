using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL2
{
    public partial class Phrases
    {
        public Guid Id { get; set; }
        public string Phrase { get; set; }
        public long BuyerPopularity { get; set; }
        public long? SellerPopularity { get; set; }
    }
}
