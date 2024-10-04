using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using expense_transactions.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IActionResult> Approve(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            user.IsAdminApproved = true;
            _context.Users?.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
}
