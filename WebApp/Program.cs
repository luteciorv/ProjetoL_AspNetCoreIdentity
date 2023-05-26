using AspNetCoreIdentity.Application.Services;
using AspNetCoreIdentity.Domain.Interfaces.Repositories;
using AspNetCoreIdentity.Application.Interfaces.Services;
using AspNetCoreIdentity.Infrastructure.Data;
using AspNetCoreIdentity.Infrastructure.Interfaces;
using AspNetCoreIdentity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Extensions;
using WebApp.Policies;
using System.Reflection;
using AspNetCoreIdentity.Application.Profiles;
using WebApp.Controllers;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Cookie.Identity";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.SlidingExpiration = true;
    });

// Autoriza��o baseada em Roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserManagerAdminRole",
        policy => policy.RequireRole("User", "Manager", "Admin"));
});

// Autoriza��o baseada em Claims
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdminClaimAccess", policy =>
    {
        policy.RequireClaim("CadastradoEm");
        policy.RequireClaim("IsAdmin", "true");
    });
    options.AddPolicy("IsEmployeeClaimAccess", policy => policy.RequireClaim("IsEmployee", "true"));

    // Pol�tica de claims personalizada
    options.AddPolicy("TempoCadastroMinimo", policy => policy.Requirements.Add(new TempoCadastroRequirement(4)));
});

// Inje��o de depend�ncia
builder.Services.AddHttpClient<AlunosController>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISeedDatabase, SeedDatabase>();
builder.Services.AddScoped<IAuthorizationHandler, TempoCadastroHandler>();
builder.Services.AddAutoMapper(typeof(StudentProfile));

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

await app.SeedDatabase();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MinhaArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
