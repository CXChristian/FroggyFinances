using expense_transactions.Data;
using Microsoft.AspNetCore.Mvc;
using expense_transactions.Services;

namespace MicrosoftWebA1.Controllers
{
    public class ReportController : Controller
    {
        private readonly TransactionContext _context;
        
        public ReportController(TransactionContext transactionContext)
        {
            _context = transactionContext;
        }


        public IActionResult Index()
        {
            var transactions = _context.Transactions?.ToList();

            return View(transactions);
        }
    }
}