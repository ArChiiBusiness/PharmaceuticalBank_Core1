﻿using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL4
{
    public partial class JobParameter
    {
        public long JobId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual Job Job { get; set; }
    }
}
