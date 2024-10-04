using Microsoft.AspNetCore.Mvc;
using expense_transactions.Data;
using expense_transactions.Models;
using Microsoft.AspNetCore.Authorization;
using expense_transactions.Services;

namespace expense_transactions.Controllers
{
    [Authorize]
    public class CsvUploadController : Controller
    {
        private readonly TransactionContext _context;
        private readonly CsvParserService _csvParserService;
        private readonly BucketService _bucketService;
        public CsvUploadController(TransactionContext context,
        CsvParserService csvParserService, BucketService bucketService)
        {
            _context = context;
            _csvParserService = csvParserService;
            _bucketService = bucketService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(CsvUploadViewModel model)
        {
            if (model.UploadedFile == null || model.UploadedFile.Length == 0)
            {
                TempData["ErrorMessage"] = "No file selected or file size is zero.";
                return RedirectToAction("Index");
            }

            var fileExtension = Path.GetExtension(model.UploadedFile.FileName);
            if (fileExtension == null || !fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "Invalid file type. Please upload a CSV file.";
                return RedirectToAction("Index");
            }

            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var filePath = Path.Combine(uploadDirectory, model.UploadedFile.FileName);

            if (System.IO.File.Exists(filePath))
            {
                TempData["ErrorMessage"] = $"File '{model.UploadedFile.FileName}' has already been uploaded.";
                return RedirectToAction("Index");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.UploadedFile.CopyToAsync(stream);
            }

            var transactions = _csvParserService.ParseCsvToTransactions(filePath);
            _context.Transactions.AddRange(transactions);
            await _context.SaveChangesAsync();

            _bucketService.CategorizeAllTransactions();

            TempData["SuccessMessage"] = "File uploaded and processed successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearTransactions()
        {
            _context.Transactions.RemoveRange(_context.Transactions);
            _context.SaveChanges();
            ViewBag.Message = "All transactions have been deleted!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearBuckets()
        {
            _bucketService.ClearBucketsTable();
            return RedirectToAction("Index");
        }
    }


}