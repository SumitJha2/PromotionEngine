using PromotionEngine.Business;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            string decision;
            CheckoutBAL checkout = new CheckoutBAL();
            ProductBAL objProduct = new ProductBAL();
            IProduct product = new Product();
            Console.WriteLine("====================================");
            try
            {
                do
                {
                    Console.WriteLine("Enter Product");
                    string productName = Console.ReadLine();
                    if (objProduct.IsValidProduct(productName))
                    {
                    Console.WriteLine("Enter QTY");
                    string qty = Console.ReadLine();
                    product = objProduct.GetProductFromProductName(productName.ToUpper());
                    product.Qty = Convert.ToInt32(qty);
                    checkout.AddProduct(product);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Product SKU");
                    }
                    Console.WriteLine("Do you want to add another product Y | N");
                    decision = Console.ReadLine();
                }
                while (decision.ToUpper() == "Y");
                float totalPrice = checkout.Total();
                Console.WriteLine("Total= {0}", totalPrice);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Some error occured, Please try again later with valid input");
                Console.Read();
            }
        }
    }
}
