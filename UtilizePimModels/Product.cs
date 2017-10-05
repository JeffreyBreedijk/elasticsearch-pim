using System.Collections.Generic;

namespace UtilizePimModels
{
    public class Product
    {
        public string Id { get; set; }
        public Dictionary<string, string> ShortDescription { get; set; }
        public Dictionary<string, string> LongDescription { get; set; }
        public Dictionary<string, double> Unit { get; set; }
        public Dictionary<string, object> Properties { get; set; } 
        public List<string> Categories { get; set; }
        
    }
}