using e_commerce_system.Context;
using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainAppDbContet>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"))

);
builder.Services.AddIdentity<User, Role>(options =>
{

    options.Password.RequireDigit= true;
    options.Password.RequireLowercase= false;
    options.Password.RequireUppercase= false;
    options.Password.RequiredLength = 5;

})
    .AddEntityFrameworkStores<MainAppDbContet>()
    .AddDefaultTokenProviders();
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
