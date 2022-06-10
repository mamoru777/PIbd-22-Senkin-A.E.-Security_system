﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemFileImplement.Models
{
    public class Secure
    {
        public int Id { get; set; }
        public string SecureName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> SecureComponents { get; set; }
    }
}