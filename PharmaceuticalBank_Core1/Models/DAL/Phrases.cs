using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL
{
    public partial class Phrases
    {
        public Guid Id { get; set; }
        public string Phrase { get; set; }
        public long Popularity { get; set; }
    }
}
