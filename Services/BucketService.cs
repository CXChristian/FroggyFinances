using System.Text.RegularExpressions;
using expense_transactions.Data;
using expense_transactions.Models;

namespace expense_transactions.Services;

public class BucketService
{
    private readonly BucketContext _bucketContext;
    private readonly TransactionContext _transactionContext;

    public BucketService(BucketContext bucketContext, TransactionContext transactionContext)
    {
        _bucketContext = bucketContext;
        _transactionContext = transactionContext;
    }

    public string CategorizeTransaction(TransactionModel transaction)
    {
        var bucketCompany = transaction.Company?.Trim().ToUpperInvariant(); //not sure if needed
        var bucketsArray = _bucketContext.Buckets?.ToList() ?? new List<Bucket>();

        string bucketCategory = "Uncategorized";
        // int? bucketId = null;

        foreach (var bucket in bucketsArray)
        {
            if (!string.IsNullOrEmpty(bucketCompany) &&
                !string.IsNullOrEmpty(bucket.Company) &&
                bucketCompany.Contains(bucket.Company.ToUpper()))
            {
                bucketCategory = bucket.Category ?? "Uncategorized";
                // bucketId = bucket.Id;
                break;
            }
        }

        transaction.BucketCategory = bucketCategory;
        // transaction.BucketId = bucketId;
        _transactionContext.Transactions.Update(transaction);
        _transactionContext.SaveChanges();
        return bucketCategory;
    }

    public void CategorizeAllTransactions()
    {
        var transactions = _transactionContext.Transactions.ToList();
        foreach (var transaction in transactions)
        {
            CategorizeTransaction(transaction);
        }
        _transactionContext.SaveChanges();
    }

    public void ClearBucketsTable()
    {
        if (_bucketContext.Buckets == null)
        {
            return;
        }
        var allBuckets = _bucketContext.Buckets?.ToList();
        if (allBuckets == null || allBuckets.Count == 0)
        {
            return;
        }

        _bucketContext.Buckets?.RemoveRange(allBuckets);
        _bucketContext.SaveChanges();
    }
}
