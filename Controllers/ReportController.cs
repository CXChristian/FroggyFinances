using expense_transactions.Data;
using Microsoft.AspNetCore.Mvc;
using expense_transactions.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MicrosoftWebA1.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly TransactionContext _context;

        public ReportController(TransactionContext transactionContext)
        {
            _context = transactionContext;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var transactions = _context.Transactions
                .Where(t => t.userID == userId)
                .ToList();

            return View(transactions);
        }
    }
}