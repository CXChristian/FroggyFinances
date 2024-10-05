using expense_transactions.Models;
using expense_transactions.Services;
using Microsoft.AspNetCore.Mvc;

namespace expense_transactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // POST: api/Transactions
        [HttpPost]
        public IActionResult AddTransaction([FromBody] TransactionModel transaction)
        {
            if (transaction == null || string.IsNullOrEmpty(transaction.Company))
            {
                return BadRequest("Invalid transaction data.");
            }

            var success = _transactionService.AddTransaction(transaction);

            if (!success)
            {
                return BadRequest("Failed to categorize or add transaction.");
            }

            return Ok("Transaction added and categorized successfully.");
        }

        // GET: api/Transactions/byBucketCategory/{category}
        [HttpGet("byBucketCategory/{category}")]
        public IActionResult GetTransactionsByBucket(string category)
        {
            var transactions = _transactionService.GetTransactionsByBucket(category);
            return Ok(transactions);
        }

        // GET: api/Transactions
        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var transactions = _transactionService.GetAllTransactions();
            return Ok(transactions);
        }

        // GET: api/Transactions/categorizeAll
        [HttpGet("categorizeAll")]
        public IActionResult CategorizeAllTransactions()
        {
            _transactionService.CategorizeAllTransactions();
            return Ok("All transactions have been categorized.");
        }
    }
}