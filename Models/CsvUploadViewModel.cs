using System;
using System.Transactions;

namespace expense_transactions.Models;

public class CsvUploadViewModel
{
    public required IFormFile UploadedFile { get; set; }

    public string? StatusMessage { get; set; }
    public bool IsSuccess { get; set; }
    public List<TransactionModel>? UploadedTransactions { get; set; }
}
