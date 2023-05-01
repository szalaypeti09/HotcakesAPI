using NUnit.Framework;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hotcakes.Commerce.Catalog;
using Hotcakes.Commerce.Dnn.Prompt;
using Hotcakes.CommerceDTO.v1.Client;

namespace UnitTest_HCAPI
{
    internal class API_verification
    {
        private Api api;

        [SetUp]
        public void Setup()
        {
            // Initialize the API client
            api = new Api("http://20.234.113.211:8083", "1-6bd2d3e3-d6ff-4d43-80de-4e1efab85207");
        }

        [Test]
        public void TestProductsFindAll()
        {
   
            var result = api.ProductsFindAll();
     
            Assert.NotNull(result);
            Assert.IsNotEmpty(result.Content);

        }
    }
}
