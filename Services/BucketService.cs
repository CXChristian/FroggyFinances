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
        Console.WriteLine("Starting CategorizeTransaction method...");  // Debug print

        var bucketCompany = transaction.Company?.Trim().ToUpperInvariant();
        Console.WriteLine($"Normalized company name: '{bucketCompany}'");

        var bucketMapping = new Dictionary<string, string>
        {
            { @"\b(WALMART|REAL\s*CDN\s*SUPERS|COSTCO)\b", "Groceries" },  // Include optional spaces in "REAL CDN SUPERS"
            { @"\b(SUBWAY|MCDONALDS|TIM\s*HORTONS|STARBUCKS|RESTAU|ST\s*JAMES\s*RESTAURAT|WHITE\s*SPOT\s*RESTAURAN)\b", "Dining" },
            { @"\b(ICBC|WORLD\s*VISION|MSP|INS|INSURANCE)\b", "Insurance" },  // Use WORLD VISION instead of WORD VISION
            { @"\b(ROGERS|SHAW|FORTISBC|CABLE)\b", "Utilities" },
            { @"\b(O.D.P|MONTHLY\s*ACCOUNT\s*FEE|CHQ|GATEWAY|RED\s*CROSS)\b", "Miscellaneous" }
        };

        // Use the helper method to find the bucket category based on the mapping
        var bucketCategory = !string.IsNullOrEmpty(bucketCompany) ? FindMatchingBucketCategory(bucketCompany, bucketMapping) : "Uncategorized";
        Console.WriteLine($"Bucket category found: '{bucketCategory}'");

        // Check if a bucket with the same category already exists in the database
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

        // Add transaction to bucket and set FK ref
        if (!bucket.Transactions.Contains(transaction))
        {
            bucket.Transactions.Add(transaction);
            transaction.BucketId = bucket.Id;
            transaction.Bucket = bucket;
            _transactionContext.Update(transaction);
            Console.WriteLine($"Transaction #{transaction.Id} added to bucket {bucket.Category}");
        }
        return bucket;
    }

    // This helper method contains the logic for iterating through the bucket mapping
    public string FindMatchingBucketCategory(string company, Dictionary<string, string> bucketMapping)
    {
        foreach (var mapping in bucketMapping)
        {
            // Check if the company name matches any regex pattern in the mapping dictionary
            if (Regex.IsMatch(company, mapping.Key, RegexOptions.IgnoreCase))
            {
                Console.WriteLine($"Match found for company '{company}' with pattern '{mapping.Key}' and category '{mapping.Value}'");
                return mapping.Value;  // Return the matched category
            }
        }
        return "Uncategorized";  // Default category if no match is found
    }

    // Adjust GetBucketCategory to call FindMatchingBucketCategory
    public string GetBucketCategory(string company, Dictionary<string, string> bucketMapping)
    {
        return FindMatchingBucketCategory(company, bucketMapping);
    }

    public void CategorizeAllTransactions()
    {
        var transactions = _transactionContext.Transactions.ToList();
        Console.WriteLine($"Total transactions to categorize: {transactions.Count}");

        foreach (var transaction in transactions)
        {
            Console.WriteLine($"Categorizing transaction: ID={transaction.Id}, Company={transaction.Company}, Amount={transaction.Amount}");
            var bucket = CategorizeTransaction(transaction);

            if (!bucket.Transactions.Contains(transaction))
            {
                Console.WriteLine($"Adding transaction ID={transaction.Id} to bucket category '{bucket.Category}'");
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