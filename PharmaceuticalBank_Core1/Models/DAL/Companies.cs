using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL
{
    public partial class Companies
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string Route { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string GlobalHq { get; set; }
        public string GlobalHqaddress { get; set; }
        public string GlobalHqduns { get; set; }
        public string DomesticHq { get; set; }
        public string DomesticHqaddress { get; set; }
        public string DomesticHqduns { get; set; }
        public double? Revenue { get; set; }
        public double? EmployeesCount { get; set; }
        public double? TotalNumberOfShipments { get; set; }
        public double? NumberOfMatchedShipments { get; set; }
        public string PanjivaUrl { get; set; }
        public string Top3Customers { get; set; }
        public string Top5Products { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public double? WeightOfMatchingShipments { get; set; }
        public DateTime? LastShipmentDateOfMatchedShipments { get; set; }
        public double? ValueOfMatchingChinaTradeData { get; set; }
        public Guid? Id { get; set; }
    }
}
