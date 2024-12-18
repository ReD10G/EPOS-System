using System.Collections.Generic;

namespace SERC_EPOS
{
    internal class TransactionForm
    {
        private List<string> transactions;

        public TransactionForm(List<string> transactions)
        {
            this.transactions = transactions;
        }
    }
}