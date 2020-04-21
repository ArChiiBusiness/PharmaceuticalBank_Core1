using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL3
{
    public partial class Counter
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
