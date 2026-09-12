using e_commerce_system.Context;
using e_commerce_system.IServices;
using e_commerce_system.Models.Identity;
using e_commerce_system.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace e_commerce_system.Startup
{
    
public static class IServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options=>
{
    
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter  JWT token."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

 


        });

services.AddDbContext<MainAppDbContet>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))

);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<PaginationService>();
            services.AddScoped<ICategorieService, CategorieService>();
            services.AddScoped<IFileImageService, FIleServiceImage>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ICartService, CartService>(); 
            services.AddScoped<ICartSessionService, CartSessionService>();
            services.AddScoped<IOrderService, OrderService>();

services.AddIdentity<User, Role>(options =>
{

    options.Password.RequireDigit= true;
    options.Password.RequireLowercase= false;
    options.Password.RequireUppercase= false;
    options.Password.RequiredLength = 5;

})
    .AddEntityFrameworkStores<MainAppDbContet>()
    .AddDefaultTokenProviders();

services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateIssuer = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience= configuration["Jwt:Audience"],

        ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["jwt:Key"]))
	};
});

           
            return services;
        


}

    }



}