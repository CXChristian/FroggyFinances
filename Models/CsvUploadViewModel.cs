using System;
using System.Transactions;

namespace expense_transactions.Models;

public class CsvUploadViewModel
{
    public required IFormFile UploadedFile { get; set; }
}
