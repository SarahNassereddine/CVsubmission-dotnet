using lab5.Data;
using lab5.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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

//Admin login: cookie-based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

var app = builder.Build();
using (var scope = app.Services.CreateScope())

{

    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();

}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // must come BEFORE UseAuthorization
app.UseAuthorization();

app.MapRazorPages();

app.Run();
