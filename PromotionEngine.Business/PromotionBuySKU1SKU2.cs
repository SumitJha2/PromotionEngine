using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Business
{
    public class PromotionBuySKU1SKU2
    {
        List<ProductPromotionSKU12> _productPromotion = new List<ProductPromotionSKU12>();
        public PromotionBuySKU1SKU2()
        {
            _productPromotion = new List<ProductPromotionSKU12>(){
             new ProductPromotionSKU12{SKUID1="C",SKUID2="D",Price=30}
            };
        }
        public bool IsPromotionExistForProductCombination(string skuId1,string skuId2)
        {
            return _productPromotion.Where(x => (x.SKUID1 == skuId1 && x.SKUID2==skuId2) || (x.SKUID1==skuId2 && x.SKUID2==skuId1)).Any();
        }
        public bool IsPromotioExistForProduct(string skuId)
        {
            return _productPromotion.Where(x => x.SKUID1 == skuId || x.SKUID2 == skuId).Any();
        }


        public ProductPromotionSKU12 GetPromotionalProduct(string skuId1, string skuId2)
        {
            return _productPromotion.Where(x => (x.SKUID1 == skuId1 && x.SKUID2 == skuId2) || (x.SKUID1 == skuId2 && x.SKUID2 == skuId1)).FirstOrDefault();
        }
    }

    public class ProductPromotionSKU12
    {
        public string SKUID1 { get; set; }
        public string SKUID2 { get; set; }
        public float Price { get; set; }
    }
}
