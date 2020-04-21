using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL3
{
    public partial class Companies
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public string Website1 { get; set; }
        public string Website2 { get; set; }
        public string Profile { get; set; }
        public string Dunsa { get; set; }
        public string Industry { get; set; }
        public double? Recenue { get; set; }
        public double? Employees { get; set; }
        public double? MarketCapitalization { get; set; }
        public string TradeRoles { get; set; }
        public string Siccodes { get; set; }
        public string StockTickers { get; set; }
        public string UltimateParent { get; set; }
        public string UltimateParentWebsite { get; set; }
        public string UltimateParentHeadquartersAddress { get; set; }
        public string UltimateParentProfile { get; set; }
        public string UltimateParentStockTickers { get; set; }
    }
}
