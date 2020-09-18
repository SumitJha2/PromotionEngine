using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Model
{
   public class Product : IProduct
    {
        public string SKUID { get; set; }
        public float UnitPrice { get; set; }
        public int Qty { get; set; }
    }
   
}
