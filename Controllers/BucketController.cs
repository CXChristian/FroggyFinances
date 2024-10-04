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
    [Authorize(Roles = "admin")]
    public class BucketController : Controller
    {
        private readonly BucketContext _context;

        public BucketController(BucketContext context)
        {
            _context = context;
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bucket bucket)
        {
            if (ModelState.IsValid)
            {
                bucket.Company = bucket.Company.ToUpper();
                bucket.Category = CapitalizeFirstLetters(bucket.Category);
                _context.Add(bucket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bucket);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bucket bucket)
        {
            if (id != bucket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bucket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BucketExists(bucket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bucket);
        }

        private bool BucketExists(int id)
        {
            return _context.Buckets.Any(e => e.Id == id);
        }

        public static string CapitalizeFirstLetters(string input)
        {
            var words = input.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = words[i].ToLower();
                    var firstLetter = words[i][0];
                    words[i] = words[i].Remove(0, 1).Insert(0, firstLetter.ToString().ToUpper());
                }
            }

            return string.Join(' ', words);
        }

    }
}