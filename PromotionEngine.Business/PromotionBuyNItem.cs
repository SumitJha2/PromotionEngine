using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Business
{
    public class PromotionBuyNItem 
    {
        List<ProductPromotionBuyN> _productPromotion = new List<ProductPromotionBuyN>();
        public PromotionBuyNItem()
        {
            _productPromotion= new List<ProductPromotionBuyN>(){
            new ProductPromotionBuyN{SKUID="A",Qty=3,Price=130},
            new ProductPromotionBuyN{SKUID="B",Qty=2,Price=45}           
            };
        }
        public ProductPromotionBuyN GetPromotionalProduct(string skuId,int qty)
        {
           return _productPromotion.Where(x => x.SKUID == skuId && qty>= x.Qty).FirstOrDefault();
        }
        public bool IsPromotioExistForProduct(string skuId)
        {
            return _productPromotion.Where(x => x.SKUID == skuId).Any();
        }

    }
    public class ProductPromotionBuyN
    {
        public string SKUID { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
    }
}
