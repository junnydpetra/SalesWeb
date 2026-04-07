using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<SalesWebContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("SalesWebContext"),
        x => x.MigrationsAssembly("SalesWeb")));

builder.Services.AddScoped<SellersService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();