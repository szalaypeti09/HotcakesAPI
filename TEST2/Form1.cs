using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Text.RegularExpressions;
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
                    textBox1.Text = selectedProduct.SitePrice.ToString();
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

            if (!string.IsNullOrWhiteSpace(txtNewPrice.Text))
            {

                if (Regex.IsMatch(txtNewPrice.Text, @"^\d+(\.\d{1,2})?$"))
                {
                    if (decimal.TryParse(txtNewPrice.Text, out decimal newPrice))
                    {
                        var selectedProduct = products.Content.FirstOrDefault(p => p.ProductName == lstProducts.SelectedItem.ToString());
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
                else
                {
                    MessageBox.Show("Invalid price format. Please enter a valid price (e.g. 25.99)");
                }
            }
            else
            {
                MessageBox.Show("Please enter a price before updating");
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}