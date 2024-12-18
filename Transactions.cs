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
    public partial class Transactions : Form
    {
        private MainForm mainform;
        private List<string> transactions;
        public Transactions(List<string> transactions)
        {
            InitializeComponent();

            if (transactions == null || transactions.Count == 0)
            {
                MessageBox.Show("No transactions available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Populate the ListBox with transactions
            foreach (var transaction in transactions)
            {
                LstBxTransactions.Items.Add(transaction.Split('\n')[0]); // Add the transaction date as a summary
            }

            // Attach the event handler for selection
            LstBxTransactions.SelectedIndexChanged += LstBxTransactions_SelectedIndexChanged;
        }

        public Transactions(MainForm mainForm)
        {
            mainform = mainForm;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LstBxTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstBxTransactions.SelectedItem != null)
            {
                int selectedIndex = LstBxTransactions.SelectedIndex;

                if (selectedIndex >= 0 && selectedIndex < transactions.Count)
                {
                    string transactionDetails = transactions[selectedIndex];

                    MessageBox.Show(transactionDetails, "Transaction Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LstBxTransactions.ClearSelected();
                }
            }
        }
    }
}
