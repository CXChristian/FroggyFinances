using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using expense_transactions.Data;

namespace expense_transactions.Controllers;

public class UserController : Controller
{
    private readonly UserContext _context;

    public UserController(UserContext context)
    {
        _context = context;
    }


    [Authorize(Roles = "admin")]
    public IActionResult Index()
    {
        var users = _context.Users?.ToList();

        return View(users);
    }
}
