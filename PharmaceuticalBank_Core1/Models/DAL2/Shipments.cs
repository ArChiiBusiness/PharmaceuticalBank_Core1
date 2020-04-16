using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL2
{
    public partial class Shipments
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime? Date { get; set; }
        public string MatchingFields { get; set; }
        public string Consignee { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeCity { get; set; }
        public string ConsigneeStateRegion { get; set; }
        public string ConsigneePostalCode { get; set; }
        public string ConsigneeCountry { get; set; }
        public string ConsigneeFullAddress { get; set; }
        public string ConsigneeEmail1 { get; set; }
        public string ConsigneeEmail2 { get; set; }
        public string ConsigneeEmail3 { get; set; }
        public string ConsigneePhone1 { get; set; }
        public string ConsigneePhone2 { get; set; }
        public string ConsigneePhone3 { get; set; }
        public string ConsigneeFax { get; set; }
        public string ConsigneeWebsite1 { get; set; }
        public string ConsigneeWebsite2 { get; set; }
        public string ConsigneeProfile { get; set; }
        public string ConsigneeDUNSâ { get; set; }
        public string ConsigneeIndustry { get; set; }
        public string ConsigneeRevenue { get; set; }
        public double? ConsigneeEmployees { get; set; }
        public string ConsigneeMarketCapitalization { get; set; }
        public string ConsigneeTradeRoles { get; set; }
        public string ConsigneeSicCodes { get; set; }
        public string ConsigneeStockTickers { get; set; }
        public string ConsigneeUltimateParent { get; set; }
        public string ConsigneeUltimateParentWebsite { get; set; }
        public string ConsigneeUltimateParentHeadquartersAddress { get; set; }
        public string ConsigneeUltimateParentProfile { get; set; }
        public string ConsigneeUltimateParentStockTickers { get; set; }
        public string Shipper { get; set; }
        public string ShipperAddress { get; set; }
        public string ShipperCity { get; set; }
        public string ShipperStateRegion { get; set; }
        public string ShipperPostalCode { get; set; }
        public string ShipperCountry { get; set; }
        public string ShipperFullAddress { get; set; }
        public string ShipperEmail1 { get; set; }
        public string ShipperEmail2 { get; set; }
        public string ShipperEmail3 { get; set; }
        public string ShipperPhone1 { get; set; }
        public string ShipperPhone2 { get; set; }
        public string ShipperPhone3 { get; set; }
        public string ShipperFax { get; set; }
        public string ShipperWebsite1 { get; set; }
        public string ShipperWebsite2 { get; set; }
        public string ShipperProfile { get; set; }
        public string ShipperDUNSâ { get; set; }
        public string ShipperIndustry { get; set; }
        public double? ShipperRevenue { get; set; }
        public double? ShipperEmployees { get; set; }
        public string ShipperMarketCapitalization { get; set; }
        public string ShipperTradeRoles { get; set; }
        public string ShipperSicCodes { get; set; }
        public string ShipperStockTickers { get; set; }
        public string ShipperUltimateParent { get; set; }
        public string ShipperUltimateParentWebsite { get; set; }
        public string ShipperUltimateParentHeadquartersAddress { get; set; }
        public string ShipperUltimateParentProfile { get; set; }
        public string ShipperUltimateParentStockTickers { get; set; }
        public string Scac { get; set; }
        public string ShipmentOrigin { get; set; }
        public string ShipmentDestination { get; set; }
        public string PortOfLading { get; set; }
        public string PortOfLadingCountry { get; set; }
        public string PortOfLadingUnLocode { get; set; }
        public string PortOfUnlading { get; set; }
        public string PortOfUnladingCountry { get; set; }
        public string PortOfUnladingUnLocode { get; set; }
        public string TransportMethod { get; set; }
        public string HsCode { get; set; }
        public string GoodsShipped { get; set; }
        public string IsContainerized { get; set; }
        public double? VolumeTeu { get; set; }
        public double? WeightKg { get; set; }
        public double? ValueUsd { get; set; }
        public string DataSource { get; set; }
        public string DataSourceTradeDirection { get; set; }
    }
}
