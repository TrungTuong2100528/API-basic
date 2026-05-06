
//thêm vào
using HocGadgetShopAPI.Business.Interfaces;
using HocGadgetShopAPI.Business;
using HocGadgetShopAPI.Repository.Interfaces;
using HocGadgetShopAPI.Repository;
using HocGadgetShopAPI.Infrastructure;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HocGadgetShopAPI.Service;
using HocGadgetShopAPI.Service.Interfaces;
var MyAllowSecificOrigins = "_MyAllowSecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// ================== SERVICES ==================

builder.Services.AddControllers();
//DI
builder.Services.AddScoped<DbConnectionFactory>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtService>();


// JWT 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSecificOrigins,
        builder =>
        {
            builder.WithOrigins(
                "http://localhost:4200", // Angular
                "http://localhost:5173"  // React
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });

});

// ================== BUILD ==================
var app = builder.Build();

// ================== MIDDLEWARE ==================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ủy quyền/ gọi chức năng
app.UseCors(MyAllowSecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
