﻿using System.Collections.Generic;

namespace UtilizePim.Models
{
    public class Product
    {
        public string Id { get; set; }
        public bool ShowOnWebshop { get; set; }
        public bool IsDeleted { get; set; }
        public Dictionary<string, string> ShortDescription { get; set; }
        public Dictionary<string, string> LongDescription { get; set; }
        public Dictionary<string, double> Unit { get; set; }
        public Dictionary<string, object> Properties { get; set; } 
        public HashSet<string> Categories { get; set; }
        public string ProductVariantKey { get; set; }
        public HashSet<string> CustomerGrants { get; set; }
        public HashSet<string> CustomerExclusions { get; set; }
        public Dictionary<string, Dictionary<double, double>> Price { get; set; }
        
    }
}