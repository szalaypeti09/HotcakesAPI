using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace UnitTest_HCAPI
{

    public class InvalidPriceInput
    {


        [TestCase("25,00", false)]
        [TestCase("-15.99", false)]
        [TestCase("", false)]
        [TestCase("abc", false)]
        [TestCase("25.123", false)]
        [TestCase("12.3456", false)]
        [TestCase("1.2.3", false)]
        [TestCase("10.5", true)]
        [TestCase("10.99", true)]
        [TestCase("0", true)]
        public void IsValidPriceInput(string input, bool expected)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(input))
            {
                if (Regex.IsMatch(input, @"^\d+(\.\d{1,2})?$"))
                {
                    if (decimal.TryParse(input, out decimal price))
                    { 

                            result = true;
                        
                    }
                }
            }

            Assert.AreEqual(expected, result);
        }
    }}

