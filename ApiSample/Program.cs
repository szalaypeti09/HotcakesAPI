using System;
using System.Linq;
using Hotcakes.CommerceDTO.v1.Client;

namespace ApiSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("This is an API Sample Program for Hotcakes");
            Console.WriteLine();

            var url = string.Empty;
            var key = string.Empty;

            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    url = args[0];
                    key = args[1];
                }
            }

            if (url == string.Empty) url = "http://20.234.113.211:8083";
            if (key == string.Empty) key = "1-6bd2d3e3-d6ff-4d43-80de-4e1efab85207";

            var proxy = new Api(url, key);

            // Retrieve a list of products
            var products = proxy.ProductsFindAll();

            // Find the product you want to update
            Console.Write("Enter the Bvin of the product you want to update: ");
            var bvin = Console.ReadLine();

           var product = products.Content.ToList().FirstOrDefault(p => p.Bvin == bvin);
           
            if (product != null)
            {
                // Update the price of the product
                Console.WriteLine($"Current price of {product.ProductName}: {product.SitePrice}");
                Console.Write($"Enter new price for {product.ProductName}: ");
                double newPrice = Convert.ToDouble(Console.ReadLine());
                product.SitePrice = Convert.ToDecimal(newPrice);

                var updateResult = proxy.ProductsUpdate(product);

                if (updateResult.Errors.Count > 0)
                {
                    // Handle any errors that occurred
                    Console.WriteLine("Error updating product: " + updateResult.Errors[0].Description);
                }
                else
                {
                    Console.WriteLine($"Price of {product.ProductName} updated successfully to {product.SitePrice}");
                }
            }
            else
            {
                Console.WriteLine("Product not found");
            }

            Console.WriteLine("Done - Press a key to continue");
            Console.ReadKey();
        }
    }
}
