using System;

namespace expense_transactions.Models;

public class CsvUploadViewModel
/**
    * Represents the uploaded CSV file.
    */
{
    public required IFormFile UploadedFile { get; set; }
}
