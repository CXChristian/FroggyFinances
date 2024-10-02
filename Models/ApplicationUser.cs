using Microsoft.AspNetCore.Identity;


namespace expense_transactions.Models
{
    public class ApplicationUser : IdentityUser
    {
        
                
        public string? Role { get; set; }
    }
}