using expense_transactions.Data;
using Microsoft.AspNetCore.Mvc;
using expense_transactions.Services;

namespace MicrosoftWebA1.Controllers
{
    public class ReportController : Controller
    {
        private readonly TransactionContext _transactionContext;
        private readonly BucketService _bucketService;
        
        public ReportController(TransactionContext transactionContext, BucketService bucketService)
        {
            _transactionContext = transactionContext;
            _bucketService = bucketService;
        }


        public IActionResult Index()
        {
            var transactions = _transactionContext.Transactions?.ToList();

            return View(transactions);
        }
    }
}