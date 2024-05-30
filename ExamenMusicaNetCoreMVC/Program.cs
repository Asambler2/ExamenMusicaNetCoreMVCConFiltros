using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExamenMusicaNetCoreMVC.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ExamenMusicaNetCoreMVCContext>(options =>
    options.UseSqlServer("server=musicagrupos.database.windows.net;database=GrupoC;user=as;password=P0t@t0P0t@t0;"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
