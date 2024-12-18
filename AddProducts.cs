using SERC_EPOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SERC_EPOS_Assignment_2
{
    public partial class AddProducts : Form
    {
        private MainForm mainform;
        public AddProducts(MainForm form)
        {
            InitializeComponent();
            mainform = form;
        }

        private void btnAddP_Click(object sender, EventArgs e)
        {
            string name = txtbxName.Text.Trim();
            string ingredients = txtbxIngredients.Text.Trim();
            bool isVegetarian = chkBxV.Checked;
            decimal price;
            int stock;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ingredients) || 
                !decimal.TryParse(txtbxPrice.Text, out price) || !int.TryParse(txtbxStock.Text, out stock))
            {
                MessageBox.Show("Please enter vaild details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mainform.AddProducts(name, price);

            txtbxName.Clear();
            txtbxIngredients.Clear();
            chkBxV.Checked = false;
            txtbxPrice.Clear();
            txtbxStock.Clear();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddProducts_Load(object sender, EventArgs e)
        {

        }
    }
}
