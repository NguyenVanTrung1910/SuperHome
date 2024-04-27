using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataHome.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataHomeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestVersionContext") ?? throw new InvalidOperationException("Connection string 'TestVersionContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)//xác th?c ???c th?c thi b?ng cookie Authentication
    .AddCookie(option =>
    {
        option.LoginPath = "/Home/Login";//khi m?t ng??i truy c?p vào ph?n không ???c phép s? t? ??ng h??ng ??n trang này
        option.ExpireTimeSpan = TimeSpan.FromMinutes(100); //th?i gian t?n t?i c?a cookie
    }

    );
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
app.UseSession();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
