using e_commerce_system.Context;
using e_commerce_system.Enum;
using e_commerce_system.IServices;
using e_commerce_system.Models.Identity;
using e_commerce_system.Services;
using e_commerce_system.Startup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices(builder.Configuration);

    
var app = builder.Build();
using (var Scope = app.Services.CreateScope())
{

    var servicee = Scope.ServiceProvider;
    var rolmananger = servicee.GetRequiredService<RoleManager<Role>>();
     SeedRoles(rolmananger).GetAwaiter().GetResult();
}

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


static async Task SeedRoles(RoleManager<Role> role)
{
    foreach(var RoleName in Enum.GetValues(typeof(UserRole))){

        var NormalizeRole = RoleName.ToString().ToLower();

        if (!await role.RoleExistsAsync(NormalizeRole))
        {
          await  role.CreateAsync(new Role { Name = NormalizeRole });

        }
    }

}