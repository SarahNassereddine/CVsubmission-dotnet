using lab5.Data;
using lab5.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ① Load connection string from appsettings.json
var connString = builder.Configuration
    .GetConnectionString("DefaultConnection");

// ② Register AppDbContext with DI (scoped lifetime)
builder.Services.AddDbContext<AppDbContext>(
    // ③ Choose database provider — swap for UseSqlServer, UseNpgsql…
    options => options.UseSqlite(connString!));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPhotoServices, PhotoServices>();
builder.Services.AddScoped<IDBServices, DBServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
