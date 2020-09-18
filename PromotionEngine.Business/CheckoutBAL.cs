using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Business
{
    public class CheckoutBAL : ICheckout
    {
        List<Product> lstProduct = new List<Product>();
        PromotionBuyNItem promotionByNItem = new PromotionBuyNItem();
        PromotionBuySKU1SKU2 promotionBySku1Sku2 = new PromotionBuySKU1SKU2();
        List<ProductPromotionSKU12> tempPromotion = new List<ProductPromotionSKU12>();
        float priceTotal = 0;
        List<Product> lstSku1Sku2Product = new List<Product>();
        /// <summary>
        /// Add in to the Cart provided by the user
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(IProduct product)
        {
            lstProduct.Add((Product)product);
        }
        /// <summary>
        /// Calculate the total 
        /// </summary>
        /// <returns></returns>
        public float Total()
        {
           
            var productWiseQty = GetProductWiseOrderQty();
            foreach (var product in productWiseQty)
            {
                float originalPrice = lstProduct.Where(x => x.SKUID == product.SKUID).FirstOrDefault().UnitPrice;
                if (promotionByNItem.IsPromotioExistForProduct(product.SKUID))
                {
                    GetPriceForPromotionByNItem(product.SKUID, product.totalQty);
                }
                else if (promotionBySku1Sku2.IsPromotioExistForProduct(product.SKUID))
                {
                    MarkAsSKU1SKU2Promotion(product.SKUID, product.totalQty, originalPrice);
                }
                else
                {                   
                    priceTotal = priceTotal + (product.totalQty * originalPrice);
                }
            }
            // calculate price for sku1sku2 promotion
            if(lstSku1Sku2Product.Count>0)
            {
                GetPriceForSKU1SKU2Promotion();
            }
            return priceTotal;
        }
       /// <summary>
       /// 
       /// Get skuwise total quantity
       /// Used in case if user add same sku more then once
       /// </summary>
       /// <returns></returns>
        private dynamic GetProductWiseOrderQty()
        {
            var result = lstProduct.GroupBy(o => o.SKUID).Select(g => new { SKUID = g.Key, totalQty = g.Sum(x => x.Qty) }).OrderBy(x => x.SKUID);
            return result;
        }
        private void GetPriceForPromotionByNItem(string skuId, int totQty)
        {
            float originalPrice = lstProduct.Where(x => x.SKUID == skuId).FirstOrDefault().UnitPrice;
            int remainingQty = 0;
            var promotionalProduct = promotionByNItem.GetPromotionalProduct(skuId, totQty);
            if (promotionalProduct != null)
            {
                priceTotal = priceTotal + promotionalProduct.Price;
                remainingQty = totQty - promotionalProduct.Qty;
                while (remainingQty > promotionalProduct.Qty)
                {
                    priceTotal = priceTotal + promotionalProduct.Price;
                    remainingQty = remainingQty - promotionalProduct.Qty;
                }
                priceTotal = priceTotal + (remainingQty * originalPrice);
            }
            else
            {
                priceTotal = priceTotal + (totQty * originalPrice);
            }


        }
        /// <summary>
        /// Add the list of ordered prduct which has SKU11SKU2 promotion
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="Qty"></param>
        /// <param name="unitPrice"></param>
        private void MarkAsSKU1SKU2Promotion(string skuId,int Qty,float unitPrice)
        {
            Product product = new Product();
            product.SKUID = skuId;
            product.Qty = Qty;
            product.UnitPrice = unitPrice;
            lstSku1Sku2Product.Add(product);
        }
        /// <summary>
        /// Calculate price for sku1sku2 promotion
        /// </summary>
        private void GetPriceForSKU1SKU2Promotion()
        {
            if (lstSku1Sku2Product.Count == 1)
            {
                priceTotal = priceTotal + (lstSku1Sku2Product[0].Qty * lstSku1Sku2Product[0].UnitPrice);
            }
            else
            {
                for (int x = 0; x < lstSku1Sku2Product.Count; x++)
                {
                    for (int y = 0; y < lstSku1Sku2Product.Count; y++)
                    {
                        int remainingQtyForSKU1 = 0;
                        int remainingQtyForSKU2 = 0;

                        if (lstSku1Sku2Product[x].SKUID == lstSku1Sku2Product[y].SKUID) continue;
                        var promotionProduct = promotionBySku1Sku2.GetPromotionalProduct(lstSku1Sku2Product[x].SKUID, lstSku1Sku2Product[y].SKUID);
                        if (promotionProduct != null)
                        {
                            //Check combination and its transpose exist 
                            if (!tempPromotion.Where(z => (z.SKUID1 == lstSku1Sku2Product[x].SKUID && z.SKUID2 == lstSku1Sku2Product[y].SKUID) || (z.SKUID1 == lstSku1Sku2Product[y].SKUID && z.SKUID2 == lstSku1Sku2Product[x].SKUID)).Any())
                            {
                                remainingQtyForSKU1 = lstSku1Sku2Product[x].Qty - 1;
                                remainingQtyForSKU2 = lstSku1Sku2Product[y].Qty - 1;
                                priceTotal = priceTotal + promotionProduct.Price;
                                while (remainingQtyForSKU1 > 0 && remainingQtyForSKU2 > 0)
                                {
                                    priceTotal = priceTotal + promotionProduct.Price;
                                    remainingQtyForSKU1--;
                                    remainingQtyForSKU2--;
                                }
                                priceTotal = priceTotal + (remainingQtyForSKU1 > 0 ? (remainingQtyForSKU1 * lstSku1Sku2Product[x].UnitPrice) : 0);
                                priceTotal = priceTotal + (remainingQtyForSKU2 > 0 ? (remainingQtyForSKU2 * lstSku1Sku2Product[y].UnitPrice) : 0);
                                ProductPromotionSKU12 obj = new ProductPromotionSKU12();
                                obj.SKUID1 = lstSku1Sku2Product[x].SKUID;
                                obj.SKUID2 = lstSku1Sku2Product[y].SKUID;
                                tempPromotion.Add(obj);
                            }
                        }

                        else
                        {
                            priceTotal = priceTotal + (lstSku1Sku2Product[x].Qty * lstSku1Sku2Product[x].UnitPrice) + (lstSku1Sku2Product[y].Qty * lstSku1Sku2Product[y].UnitPrice);
                        }

                    }
                }
            }
        }
    }
}
