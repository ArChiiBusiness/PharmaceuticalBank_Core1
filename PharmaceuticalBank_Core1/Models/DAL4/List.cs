using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL4
{
    public partial class List
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}
