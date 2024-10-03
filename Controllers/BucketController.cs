using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_transactions.Data;
using expense_transactions.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    public class BucketController : Controller
    {
        private readonly BucketContext _context;

        public BucketController(BucketContext context)
        {
            _context = context;
        }

        [Authorize (Roles="admin")]
        public IActionResult Index()
        {
            var buckets = _context.Buckets.ToList();

            return View(buckets);
        }
    }
}