using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using expense_transactions.Data;
using expense_transactions.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    public class BucketController : Controller
    {
        private readonly BucketContext _context;

        public BucketController(BucketContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var buckets = _context.Buckets?.ToList();

            return View(buckets);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)  // Check parameter value
            {
                return NotFound();
            }

            if (_context.Buckets == null)
            {
                return NotFound();
            }

            var bucket = await _context.Buckets
            .FirstOrDefaultAsync(m => m.Id == id);

            if (bucket == null)  // Check if the record exists
            {
                return NotFound();
            }

            return View(bucket);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bucket = _context.Buckets?.Find(id);

            if (bucket == null)
            {
                return NotFound();
            }
            _context.Buckets?.Remove(bucket);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (_context.Buckets == null)
            {
                return NotFound();
            }
            var bucket = await _context.Buckets.FindAsync(id);

            return View(bucket);
        }


    }
}