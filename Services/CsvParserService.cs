
using System.Globalization;
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

                var transaction = new TransactionModel
                {
                    Date = csvFile.GetField(0),
                    Company = csvFile.GetField(1),
                    Amount = csvFile.GetField<float>(2)
                };

                transactions.Add(transaction);
            }
        }
        return transactions;
    }
}
