using ama_zon;
using ama_zon.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using ama_zon.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddUserSecrets<FormController>();

builder.Services.AddSingleton<ama_zon.DataBase.DbConnection>();
builder.Services.AddScoped<EmpleoadoService>();

builder.Services.AddDbContext<AmaZonContext>(options =>
{
    options.UseSqlServer(builder.Configuration["hdp-server:ConnectionString"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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