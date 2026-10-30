
using LibrarieOnline.Data;
using LibrarieOnline.Models;
using LibrarieOnline.Repositories;
using LibrarieOnline.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationUser = LibrarieOnline.Models.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity cu ApplicationUser
// Nu solicitam confirmarea adresei de email.
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Repository Pattern
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

// Configurarea rolurilor utilizatorilor
using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            var roleResult =
                await roleManager.CreateAsync(new IdentityRole(role));

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Nu s-a putut crea rolul: " + role);
            }
        }
    }

    // Identificam administratorul dupa ID-ul unic al contului.
    string? personalAdminUserId =
        app.Configuration["PersonalAdminUserId"];

    if (string.IsNullOrWhiteSpace(personalAdminUserId))
    {
        throw new InvalidOperationException(
            "PersonalAdminUserId nu este configurat in appsettings.json.");
    }

    var personalAdmin =
        await userManager.FindByIdAsync(personalAdminUserId);

    if (personalAdmin == null)
    {
        throw new InvalidOperationException(
            "Contul administratorului nu exista in baza de date.");
    }

    // Verificam toate conturile existente.
    var users = await userManager.Users.ToListAsync();

    foreach (var user in users)
    {
        bool isPersonalAdmin = user.Id == personalAdmin.Id;

        string assignedRole = isPersonalAdmin ? "Admin" : "User";

        var currentRoles = await userManager.GetRolesAsync(user);

        // Eliminam doar rolurile care nu sunt corecte.
        var rolesToRemove = currentRoles
            .Where(role => role != assignedRole)
            .ToList();

        if (rolesToRemove.Count > 0)
        {
            var removeResult =
                await userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (!removeResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Nu s-au putut actualiza rolurile contului: " + user.Email);
            }
        }

        // Adaugam rolul corect numai daca lipseste.
        if (!await userManager.IsInRoleAsync(user, assignedRole))
        {
            var addResult =
                await userManager.AddToRoleAsync(user, assignedRole);

            if (!addResult.Succeeded)
            {
                throw new InvalidOperationException(
                    "Nu s-a putut atribui rolul pentru: " + user.Email);
            }
        }
    }
}


app.Run();
