
using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using expense_transactions.Models;

namespace expense_transactions.Services;

public class CsvParserService
{
    public List<TransactionModel> ParseCsvToTransactions(string path)
    {
        var transactions = new List<TransactionModel>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false //CSV has no header
        };
        using (var reader = new StreamReader(path))
        using (var csvFile = new CsvReader(reader, config))
        {
            while (csvFile.Read())
            {
                //only get debit values
                if (string.IsNullOrWhiteSpace(csvFile.GetField(2)))
                    continue;

                var companyName = csvFile.GetField(1);
                var normalizedCompanyName = NormalizeCompanyName(companyName);

                var transaction = new TransactionModel
                {
                    Date = csvFile.GetField(0),
                    Company = normalizedCompanyName
                    Amount = csvFile.GetField<float>(2)
                };

                transactions.Add(transaction);
            }
        }
        return transactions;
    }

    private string NormalizeCompanyName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "UNKNOWN";
        }

        return Regex.Replace(name, @"\s+", " ").ToUpperInvariant().Trim();
        //replaces all whitespace characters with a single space,
        //converts to uppercase
        //trims leading and trailing whitespace
    }
}