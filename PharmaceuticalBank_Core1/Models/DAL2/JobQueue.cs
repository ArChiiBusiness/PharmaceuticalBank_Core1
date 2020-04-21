﻿using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL2
{
    public partial class JobQueue
    {
        public int Id { get; set; }
        public long JobId { get; set; }
        public string Queue { get; set; }
        public DateTime? FetchedAt { get; set; }
    }
}