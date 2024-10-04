using Microsoft.EntityFrameworkCore;
using expense_transactions.Models;
using Microsoft.AspNetCore.Identity;
using expense_transactions.Data;
using expense_transactions.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddSession();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UserContext>(options => options.UseSqlite(connString));
builder.Services.AddDbContext<BucketContext>(options => options.UseSqlite(connString));
builder.Services.AddDbContext<TransactionContext>(options => options.UseSqlite(connString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserContext>();

builder.Services.AddTransient<CsvParserService>();
builder.Services.AddTransient<BucketService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userContext = services.GetRequiredService<UserContext>();
    var bucketContext = services.GetRequiredService<BucketContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    try
    {
        userContext.Database.Migrate();
        bucketContext.Database.Migrate();
        await SampleData.Initialize(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); 
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
