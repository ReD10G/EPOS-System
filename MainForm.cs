using SERC_EPOS_Assignment_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SERC_EPOS
{
    public partial class MainForm : Form
    {
        private List<MenuItem> menuItems = new List<MenuItem>();
        private List<string> transactions = new List<string>();
        private decimal totalCost = 0;

        public MainForm()
        {   
            InitializeComponent();
            InitializeMenuItems();
            this.Text = "Main Application";
        }

        private void InitializeMenuItems()
        {
            if (ListBoxProducts != null)
            {
                MessageBox.Show("Form has loaded.");
                return;
            }
            foreach (var menuitem in menuItems)
            {
                ListBoxProducts.Items.Add(menuItems);
            }
            
        }



        private void UpdateTotalLabel()
        {
            lblTotal.Text = $"Total: £{totalCost:F2}";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clearing the listbox and it will rest the total price
            ListBoxSelectedItems.Items.Clear();
            totalCost = 0;
            UpdateTotalLabel();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exits the application
            Application.Exit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var AddProducts = new AddProducts(this);
            AddProducts.ShowDialog();
        }

        public void AddProducts(string name, decimal price)
        {
            if (menuItems.Any(p => p.Name == name))
            {
                MessageBox.Show("Product already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var newItem = new MenuItem(name, price);
                menuItems.Add(newItem);
                ListBoxProducts.Items.Add(newItem);

            }
        }

        private void ListBoxProducts_DoubleClick(object sender, EventArgs e)
        {
            if (ListBoxProducts.SelectedItem is MenuItem selectedProduct)
            {
                var existingEntry = ListBoxSelectedItems.Items
                    .Cast<string>()
                    .FirstOrDefault(item => item.StartsWith(selectedProduct.Name));

                if (existingEntry != null)
                {
                    var parts = existingEntry.Split('x');
                    var quantity = int.Parse(parts[1].Split('-')[0].Trim()) + 1;
                    var newPrice = selectedProduct.Price * quantity;

                    ListBoxSelectedItems.Items[ListBoxSelectedItems.Items.IndexOf(existingEntry)] =
                        $"{selectedProduct.Name} x{quantity} - £{newPrice:F2}";
                }
                else
                {
                    ListBoxSelectedItems.Items.Add($"{selectedProduct.Name} x1 - £{selectedProduct.Price:F2}");
                }

                totalCost += selectedProduct.Price;
                UpdateTotalLabel();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (ListBoxSelectedItems.SelectedItem != null)
            {
                string selectedItem = ListBoxSelectedItems.SelectedItem.ToString();

                var parts = selectedItem.Split('x');
                var itemName = parts[0].Trim();
                var quantityPrice = parts[1].Split('-');
                var quantity = int.Parse(quantityPrice[0].Trim());
                var priceString = quantityPrice[1].Trim().Substring(1);

                if (decimal.TryParse(priceString, out decimal totalItemPrice))
                {
                    decimal unitPrice = totalItemPrice / quantity;
                    totalCost -= unitPrice;

                    if (quantity > 1)
                    {
                        var newQuantity = quantity - 1;
                        var newTotalPrice = unitPrice * newQuantity;

                        ListBoxSelectedItems.Items[ListBoxSelectedItems.Items.IndexOf(selectedItem)] =
                            $"{itemName} x{newQuantity} - £{newTotalPrice:F2}";
                    }
                    else
                    {
                        ListBoxSelectedItems.Items.Remove(selectedItem);
                    }
                }

                UpdateTotalLabel();
            }
            else
            {
                MessageBox.Show("Please select an item to remove!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            ProcessTransaction("Card");
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            ProcessTransaction("Cash");
        }

        private void ProcessTransaction(string paymentMethod)
        {
            if (ListBoxSelectedItems.Items.Count == 0)
            {
                MessageBox.Show("No items selected for transaction!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var transactionDetails = new StringBuilder();
            transactionDetails.AppendLine($"Transaction Date: {DateTime.Now}");
            transactionDetails.AppendLine($"Payment Method: {paymentMethod}");
            transactionDetails.AppendLine("Items:");

            foreach (var item in ListBoxSelectedItems.Items)
            {
                transactionDetails.AppendLine(item.ToString());
            }

            transactionDetails.AppendLine($"Total: £{totalCost:F2}");

            transactions.Add(transactionDetails.ToString()); // Save transaction for TransactionForm

            MessageBox.Show("Transaction Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListBoxSelectedItems.Items.Clear();
            totalCost = 0;
            UpdateTotalLabel();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Transactions = new Transactions(transactions);
            Transactions.ShowDialog();
        }
    }
}
           


       
  
