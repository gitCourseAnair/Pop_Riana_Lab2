using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Pop_Riana_Lab2.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pop_Riana_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Pop_Riana_Lab2Context' not found.");

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<Pop_Riana_Lab2Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

