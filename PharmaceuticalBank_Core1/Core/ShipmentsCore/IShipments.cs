using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmaceuticalBank_Core1.Models.DAL4;

namespace PB.Core.ShipmentsCore
{
    interface IShipments
    {
        public List<Shipments> SearchShipmentsByString(string criteria, int page);
    }
}
