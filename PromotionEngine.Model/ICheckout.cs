using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Model
{
   public interface ICheckout
    {
        void AddProduct(IProduct product);
        float Total();
    }
}
