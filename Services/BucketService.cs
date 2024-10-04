//TODO: put get bucket category iteration in a helper method

using System;
using System.Data;
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

    public Bucket CategorizeTransaction(TransactionModel transaction)
    {

        var bucketMapping = new Dictionary<string, string>
        {
            { "Walmart", "Groceries" },
            { "Subway", "Dining" },
            { "McDonalds", "Dining" },
            { "ICBC", "Insurance" },
            { "Real CDN SUPERS", "Groceries" },
            { "ROGERS", "Utilities" },
            { "TIM HORTONS", "Dining" },
            { "FORTISBC", "Utilities" }
        };

        string? bucketCategory = null;
        string? bucketCompany = string.Empty;

        foreach (var mapping in bucketMapping)
        {
            if ((!string.IsNullOrEmpty(transaction.Company)) &&
            transaction.Company.Contains(mapping.Key))
            {
                bucketCategory = mapping.Value;
                bucketCompany = mapping.Key;
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(bucketCategory))
        {
            bucketCategory = "Uncategorized";
        }

        var bucket = _bucketContext.Buckets?.FirstOrDefault(b => b.Category == bucketCategory);

        if (bucket == null)
        {
            bucket = new Bucket
            {
                Category = bucketCategory,
                Company = bucketCompany
            };

            _bucketContext.Buckets?.Add(bucket);
            _bucketContext.SaveChanges();
        }
        return bucket;
    }

    // private string GetBucketCategory(Dictionary<string, string> bucketMapping, string? category) {

    // }

    public void CategorizeAllTransactions()
    {
        var transactions = _transactionContext.Transactions.ToList();
        foreach (var transaction in transactions)
        {
            var bucket = CategorizeTransaction(transaction);

            if (!bucket.Transactions.Contains(transaction))
            {
                bucket.Transactions.Add(transaction);
            }
        }

        _transactionContext.SaveChanges();
        _bucketContext.SaveChanges();
    }

    public void ClearBucketsTable()
    {

        if (_bucketContext.Buckets == null)
        {
            return;
        }
        var allBuckets = _bucketContext.Buckets?.ToList();
        if (allBuckets == null || allBuckets.Count == 0) {
            return;
        }

        _bucketContext.Buckets?.RemoveRange(allBuckets); 
        _bucketContext.SaveChanges();
    }
}

