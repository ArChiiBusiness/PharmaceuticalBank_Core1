﻿using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL3
{
    public partial class Job
    {
        public long Id { get; set; }
        public long? StateId { get; set; }
        public string StateName { get; set; }
        public string InvocationData { get; set; }
        public string Arguments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }
    }
}