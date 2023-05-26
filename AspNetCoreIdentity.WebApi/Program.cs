using AspNetCoreIdentity.Application.Interfaces.Services;
using AspNetCoreIdentity.Application.Profiles;
using AspNetCoreIdentity.Application.Services;
using AspNetCoreIdentity.Domain.Interfaces.Repositories;
using AspNetCoreIdentity.Infrastructure.Data;
using AspNetCoreIdentity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=AspNetCoreIdentity;Trusted_Connection=True;MultipleActiveResultSets=true;User Id=sa;Password=sa");

});
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddAutoMapper(typeof(StudentProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
