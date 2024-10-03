using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expense_transactions.Controllers;

public class CsvUploadController: Controller
{

    private readonly DbContext _context;

    public CsvUploadController(DbContext context)
    {
        _context = context;
    }

[HttpGet]
    public IActionResult Index()
    {
        return View();
    }


}
