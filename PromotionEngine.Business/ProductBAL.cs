using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Business
{
   public class ProductBAL
    {
        public List<Product> GetProductList()
        {
            return new List<Product>(){
            new Product{SKUID="A",UnitPrice=50},
            new Product{SKUID="B",UnitPrice=30},
            new Product{SKUID="C",UnitPrice=20},
            new Product{SKUID="D",UnitPrice=15},
            new Product{SKUID="E",UnitPrice=20}
            };
        }
        /// <summary>
        /// get the product from name
        /// </summary>
        /// <param name="productName">productname</param>
        /// <returns>Product object</returns>
        public IProduct GetProductFromProductName(string skuId)
        {
            return this.GetProductList().Where(x => x.SKUID == skuId).FirstOrDefault();
        }
        public bool IsValidProduct(string skuId)
        {
            return this.GetProductList().Where(x => x.SKUID.ToUpper() == skuId.ToUpper()).Any();
        }
    }
}
