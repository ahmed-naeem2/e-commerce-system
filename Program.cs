using e_commerce_system.Context;
using e_commerce_system.Enum;
using e_commerce_system.IServices;
using e_commerce_system.Models.Identity;
using e_commerce_system.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MainAppDbContet>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"))

);

builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ICategorieService, CategorieService>();

builder.Services.AddIdentity<User, Role>(options =>
{

    options.Password.RequireDigit= true;
    options.Password.RequireLowercase= false;
    options.Password.RequireUppercase= false;
    options.Password.RequiredLength = 5;

})
    .AddEntityFrameworkStores<MainAppDbContet>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience= builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]))
	};


});

    
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