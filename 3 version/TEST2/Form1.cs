using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotcakes.Commerce.Catalog;
using Hotcakes.Commerce.Dnn.Prompt;
using Hotcakes.CommerceDTO.v1.Client;
//using Hotcakes.CommerceDTO.v1.Catalog;
//using Hotcakes.CommerceDTO.v1.Contacts;


namespace TEST2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var proxy = new Api("http://20.234.113.211:8083", "1-6bd2d3e3-d6ff-4d43-80de-4e1efab85207");

            var products = proxy.ProductsFindAll();

            foreach (var product in products.Content)
            {
                lstProducts.Items.Add(product.ProductName);
            }
            lstProducts.SelectedIndexChanged += (sender, e) =>
            {
                var selectedProduct = products.Content.FirstOrDefault(p => p.ProductName == lstProducts.SelectedItem.ToString());

                if (selectedProduct != null)
                {
                    // Update the price of the selected product
                    textBox1.Text = selectedProduct.SitePrice.ToString();

                    // Get new price from the TextBox
                    //if (decimal.TryParse(txtNewPrice.Text, out decimal newPrice))
                    //{
                    //    selectedProduct.SitePrice = newPrice;

                    //    var updateResult = proxy.ProductsUpdate(selectedProduct);

                    //    if (updateResult.Errors.Count > 0)
                    //    {
                    //        // Handle any errors that occurred
                    //        MessageBox.Show("Error updating product: " + updateResult.Errors[0].Description);
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show($"Price of {selectedProduct.ProductName} updated successfully to {selectedProduct.SitePrice}");
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Invalid price entered");
                    //}
                }
            };
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {



        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var proxy = new Api("http://20.234.113.211:8083", "1-6bd2d3e3-d6ff-4d43-80de-4e1efab85207");
            var products = proxy.ProductsFindAll();
            var selectedProduct = products.Content.FirstOrDefault(p => p.ProductName == lstProducts.SelectedItem.ToString());
            if (decimal.TryParse(txtNewPrice.Text, out decimal newPrice) ) 
            {
                selectedProduct.SitePrice = newPrice;

                var updateResult = proxy.ProductsUpdate(selectedProduct);

                txtNewPrice.Text = "";

                if (updateResult.Errors.Count > 0)
                {
                    // Handle any errors that occurred
                    MessageBox.Show("Error updating product: " + updateResult.Errors[0].Description);
                }
                else
                {
                    MessageBox.Show($"Price of {selectedProduct.ProductName} updated successfully to {selectedProduct.SitePrice} $");
                    textBox1.Text = selectedProduct.SitePrice.ToString(); // Update the text box with the new price
                }
            }
            else
            {
                MessageBox.Show("Invalid price entered");
            }
        }
    }
}

    

