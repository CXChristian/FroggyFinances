using Microsoft.AspNetCore.Mvc;
using expense_transactions.Data;
using expense_transactions.Models;
using expense_transactions.Services;

namespace expense_transactions.Controllers
{
    public class CsvUploadController : Controller
    {
        private readonly TransactionContext _context;
        private readonly CsvParserService _csvParserService;
        public CsvUploadController(TransactionContext context, CsvParserService csvParserService)
        {
            _context = context;
            _csvParserService = csvParserService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(CsvUploadViewModel model)
        {
            if (model.UploadedFile != null && model.UploadedFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.UploadedFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(stream);
                }

                var transactions = _csvParserService.ParseCsvToTransactions(path);

                
                Console.WriteLine($"Type of transactions: {transactions.GetType()}");
                Console.WriteLine($"Type of single transaction: {typeof(expense_transactions.Models.TransactionModel)}");

                _context.Transactions.AddRange(transactions);

                await _context.SaveChangesAsync();

                System.IO.File.Move(path, path + ".imported");

                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}