using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Intex2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Intex2Connection") ?? throw new InvalidOperationException("Connection string 'Intex2Connection' not found.");

builder.Services.AddDbContext<IntexDbContext>(options =>
    //options.UseSqlServer(connectionString));
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<IntexDbContext>();
// builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<IntexDbContext>();

builder.Services.AddScoped<IIntex2Repository, EFIntex2Repository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<IAdminRepository, EFAdminRepository>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication_Google_ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication_Google_ClientSecret"];
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use((context, next) =>
{
    if (context.Request.Headers["x-forwarded-proto"] == "https")
    {
        context.Request.Scheme = "https";
    }
    return next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.MapControllerRoute("roles", "Role", new { Controller = "Role", Action = "Index" });
// app.MapControllerRoute("pagenumandtype", "{category}/Page{pageNum}", new { Controller = "Home", Action = "Products" });
// app.MapControllerRoute("page", "Page/{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });
// app.MapControllerRoute("projectType", "{category}", new { Controller = "Home", Action = "Products", pageNum = 1 });
// app.MapControllerRoute("pagination", "Products/Page{pageNum}", new { Controller = "Home", Action = "Products", pageNum = 1 });
app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
