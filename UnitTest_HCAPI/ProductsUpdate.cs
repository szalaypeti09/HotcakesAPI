using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotcakes.Commerce.Catalog;
using Hotcakes.Commerce.Dnn.Prompt;
using Hotcakes.CommerceDTO.v1.Client;


namespace UnitTest_HCAPI
{
    internal class ProductsUpdate
    {
        [Test]
        
        public void TestProductsUpdate()
        {
            // Arrange
            var proxy = new Api("http://20.234.113.211:8083", "1-6bd2d3e3-d6ff-4d43-80de-4e1efab85207");
            var products = proxy.ProductsFindAll();
            var originalPrice = products.Content[0].SitePrice;

            // Act

            var selectedProduct = products.Content.FirstOrDefault(p => p.ProductName == "Test Product 1");

            if (selectedProduct != null)
            {
                selectedProduct.SitePrice += 10;

                var updateResult = proxy.ProductsUpdate(selectedProduct);

                // Assert
                Assert.IsEmpty(updateResult.Errors);
                Assert.AreEqual(originalPrice + 10, selectedProduct.SitePrice);

                // Set the price back to the original value
                selectedProduct.SitePrice = originalPrice;
                proxy.ProductsUpdate(selectedProduct);
            }
            
        }





    }
}
