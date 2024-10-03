// using System;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using expense_transactions.Models;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;

// namespace expense_transactions.Controllers;

// public class CsvUploadController : Controller
// {

//     private readonly DbContext _context;

//     public CsvUploadController(DbContext context)
//     {
//         _context = context;
//     }

//     [HttpGet]
//     public IActionResult Index()
//     {
//         return View();
//     }

//     [HttpPost]
//     public async Task<IActionResult> Upload(CsvUploadViewModel model) {
//         //check if file exists and has content
//         if (model.UploadedFile != null && model.UploadedFile.Length > 0) { 

//             //save the file in uploads folder
//             var path = Path.Combine(Directory.GetCurrentDirectory(),
//             "wwwroot/uploads", model.UploadedFile.FileName);

            
//         }

//     }
//     private static List<Transaction> ParseToTransactions(string path)
//     {

//         var transactions = new List<Transaction>();

//         using (var reader = new StreamReader(path))
//         {

//             reader.ReadLine(); //skip header!

//             while (!reader.EndOfStream)
//             {
//                 var line = reader.ReadLine();
//                 var values = line?.Split(',');

//                 //skip rows where credit amount has value (ie. not a transaction)
//                 if (!string.IsNullOrEmpty(values?[2]))
//                 {
//                     continue;
//                 }

//                 //parse only if debit amount has value

//                 if (!string.IsNullOrEmpty(values[3]))
//                 {

//                     var transaction = new Transaction
//                     {
//                         Date = values?[0],
//                         Company = values?[1],
//                         Amount = float.Parse(values?[3] ?? "0")
//                     };

//                     transactions.Add(transaction);
//                 }
//             }
//         }
//         return transactions;
//     }
// }
