using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Business;
using PromotionEngine.Model;

namespace PromotionEngine.Test
{
    [TestClass]
    public class UnitTest1
    {
        CheckoutBAL checkout = new CheckoutBAL();
        ProductBAL objProduct = new ProductBAL();
        IProduct product = new Product();
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void CalculatePrice_1A_1B_1C()
        {
            //A-3,price-1.26
            product = objProduct.GetProductFromProductName("A");
            product.Qty = 1;
            checkout.AddProduct(product);
            product = objProduct.GetProductFromProductName("B");
            product.Qty = 1;
            checkout.AddProduct(product);
            product = objProduct.GetProductFromProductName("C");
            product.Qty = 1;
            checkout.AddProduct(product);          
            float actualPrice = checkout.Total();
            float expectedPrice = float.Parse("100");
            Assert.AreEqual(actualPrice, expectedPrice, "Both price are not equal");
        }
        [TestMethod]
        public void CalculatePrice_5A_5B_1C()
        {
            //A-3,price-1.26
            product = objProduct.GetProductFromProductName("A");
            product.Qty = 5;
            checkout.AddProduct(product);
            product = objProduct.GetProductFromProductName("B");
            product.Qty = 5;
            checkout.AddProduct(product);
            product = objProduct.GetProductFromProductName("C");
            product.Qty = 1;
            checkout.AddProduct(product);
            float actualPrice = checkout.Total();
            float expectedPrice = float.Parse("370");
            Assert.AreEqual(actualPrice, expectedPrice, "Both price are not equal");
        }
        [TestMethod]
        public void CalculatePrice_3A_5B_1C_1D()
        {
            //A-3,price-1.26
            product = objProduct.GetProductFromProductName("A");
            product.Qty = 3;
            checkout.AddProduct(product);
            product = objProduct.GetProductFromProductName("B");
            product.Qty = 5;
            checkout.AddProduct(product);
            product = objProduct.GetProductFromProductName("C");
            product.Qty = 1;
            checkout.AddProduct(product);
            product = objProduct.GetProductFromProductName("D");
            product.Qty = 1;
            checkout.AddProduct(product);
            float actualPrice = checkout.Total();
            float expectedPrice = float.Parse("280");
            Assert.AreEqual(actualPrice, expectedPrice, "Both price are not equal");
        }
    }
}
