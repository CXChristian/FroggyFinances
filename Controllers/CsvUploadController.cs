//TODO: add validation if file with file name already uploaded before
//TODO: add validation for file type (only csv's)
//TODO: remove console logs after testing
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
            if (model.UploadedFile != null && model.UploadedFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.UploadedFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(stream);
                }

                if (System.IO.File.Exists(path))
                {
                    Console.WriteLine($"File {model.UploadedFile.FileName} has been successfully uploaded to {path}");
                }
                else
                {
                    Console.WriteLine("File upload failed or file not found in directory.");
                }

                var transactions = _csvParserService.ParseCsvToTransactions(path);

                Console.WriteLine($"Number of parsed transactions: {transactions.Count}");

                _context.Transactions.AddRange(transactions);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Number of transactions in the database after saving: {_context.Transactions.Count()}");
                System.IO.File.Move(path, path + ".imported");

                ViewBag.Message = "File uploaded successfully!";
                ViewBag.FilePath = path;

                //call bucket service to categorize all transactions after uploading successfully
                _bucketService.CategorizeAllTransactions();

                return RedirectToAction("Index");
            }
            ViewBag.Message = "No file selected or file size is zero.";

            return View("Index");
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