using expense_transactions.Data;
using expense_transactions.Models;
using System.Linq;

namespace expense_transactions.Services
{
    public class TransactionService
    {
        private readonly TransactionContext _transactionContext;
        private readonly BucketService _bucketService;

        public TransactionService(TransactionContext transactionContext, BucketService bucketService)
        {
            _transactionContext = transactionContext;
            _bucketService = bucketService;
        }

        // Method to validate and add a transaction with a bucket category
        public bool AddTransaction(TransactionModel transaction)
        {
            if (transaction == null)
            {
                return false;
            }

            // Use BucketService to categorize the transaction
            var category = _bucketService.CategorizeTransaction(transaction);

            // If categorization fails, set to Uncategorized
            if (category == "Uncategorized")
            {
                return false;
            }

            _transactionContext.Transactions.Add(transaction);
            _transactionContext.SaveChanges();
            return true;
        }

        // Method to get all transactions by bucket category
        public IEnumerable<TransactionModel> GetTransactionsByBucket(string category)
        {
            return _transactionContext.Transactions
                .Where(t => t.BucketCategory == category)
                .ToList();
        }

        // Method to get all transactions
        public IEnumerable<TransactionModel> GetAllTransactions()
        {
            return _transactionContext.Transactions.ToList();
        }

        // Method to categorize all transactions manually if needed
        public void CategorizeAllTransactions()
        {
            _bucketService.CategorizeAllTransactions();
        }
    }
}