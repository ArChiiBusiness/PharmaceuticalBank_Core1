using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL4
{
    public partial class Server
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public DateTime LastHeartbeat { get; set; }
    }
}
