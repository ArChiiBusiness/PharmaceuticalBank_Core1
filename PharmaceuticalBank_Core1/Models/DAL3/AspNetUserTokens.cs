﻿using System;
using System.Collections.Generic;

namespace PharmaceuticalBank_Core1.Models.DAL3
{
    public partial class AspNetUserTokens
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
