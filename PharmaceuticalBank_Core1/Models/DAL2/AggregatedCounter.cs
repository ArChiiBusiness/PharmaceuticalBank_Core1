using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL2
{
    public partial class AggregatedCounter
    {
        public string Key { get; set; }
        public long Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
