//TODO: put get bucket category iteration in a helper method

using System;
using System.Data;
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

    public Bucket CategorizeTransaction(TransactionModel transaction)
    {
        var bucketCompany = transaction.Company?.Trim().ToUpperInvariant();
        var bucketMapping = new Dictionary<string, string>
        {
            { @"\b(WALMART|REAL\s*CDN\s*SUPERS|COSTCO|SAFEWAY)\b", "Groceries" },
            { @"\b(SUBWAY|MCDONALDS|TIM\s*HORTONS|STARBUCKS|RESTAU|ST\s*JAMES\s*RESTAURAT|WHITE\s*SPOT\s*RESTAURAN)\b", "Dining" },
            { @"\b(ICBC|WORLD\s*VISION|MSP|INS|INSURANCE)\b", "Insurance" },
            { @"\b(ROGERS|SHAW|FORTISBC|CABLE)\b", "Utilities" },
            { @"\b(O.D.P|MONTHLY\s*ACCOUNT\s*FEE|CHQ|GATEWAY|RED\s*CROSS)\b", "Miscellaneous" }
        };
        var bucketCategory = !string.IsNullOrEmpty(bucketCompany) ? FindMatchingBucketCategory(bucketCompany, bucketMapping) : "Uncategorized";
        var bucket = _bucketContext.Buckets?.FirstOrDefault(b => b.Category == bucketCategory);
        if (bucket == null)
        {
            bucket = new Bucket
            {
                Category = bucketCategory,
                Company = bucketCompany,
                Transactions = new List<TransactionModel>()
            };
            _bucketContext.Buckets?.Add(bucket);
            _bucketContext.SaveChanges();
        }
        if (!bucket.Transactions.Contains(transaction))
        {
            bucket.Transactions.Add(transaction);
            transaction.BucketId = bucket.Id;
            transaction.Bucket = bucket;
            _transactionContext.Update(transaction);
        }
        return bucket;
    }

    public string FindMatchingBucketCategory(string company, Dictionary<string, string> bucketMapping)
    {
        foreach (var mapping in bucketMapping)
        {
            if (Regex.IsMatch(company, mapping.Key, RegexOptions.IgnoreCase))
            {
                return mapping.Value;
            }
        }
        return "Uncategorized";
    }

    public string GetBucketCategory(string company, Dictionary<string, string> bucketMapping)
    {
        return FindMatchingBucketCategory(company, bucketMapping);
    }

    public void CategorizeAllTransactions()
    {
        var transactions = _transactionContext.Transactions.ToList();
        foreach (var transaction in transactions)
        {
            var bucket = CategorizeTransaction(transaction);
            if (!bucket.Transactions.Contains(transaction))
            {
                bucket.Transactions.Add(transaction);
                transaction.BucketId = bucket.Id;
                _transactionContext.Update(transaction);
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
        if (allBuckets == null || allBuckets.Count == 0)
        {
            return;
        }

        _bucketContext.Buckets?.RemoveRange(allBuckets);
        _bucketContext.SaveChanges();
    }
}