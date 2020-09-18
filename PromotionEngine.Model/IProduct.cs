using System;

namespace PromotionEngine.Model
{
    public interface IProduct
    {
        string SKUID { get; set; }
        float UnitPrice { get; set; }
        int Qty { get; set; }
    }
}
