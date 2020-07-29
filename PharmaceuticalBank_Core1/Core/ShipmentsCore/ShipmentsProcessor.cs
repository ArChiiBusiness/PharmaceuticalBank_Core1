using Microsoft.EntityFrameworkCore;
using PharmaceuticalBank_Core1.Models.DAL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PB.Core.ShipmentsCore
{
    public class ShipmentsProcessor : IShipments
    {
        public List<Shipments> SearchShipmentsByString(string criteria,int page)
        {
            var db = new pharmabank1Context();
            var shipments = db.Shipments.FromSqlRaw("").ToList();
            return shipments;
        }
    }
}
