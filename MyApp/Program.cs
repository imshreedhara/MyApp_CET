using MyApp.Application.Services;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Serilog for logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Register Infrastructure services (like JwtTokenService)
builder.Services.AddInfrastructure();

// Register application services and repositories
builder.Services.AddScoped<ICETQuestionRepository, CETQuestionRepository>();
builder.Services.AddScoped<CETQuestionService>();

// ✅ Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MyApp", // 👉 change as needed
            ValidAudience = "MyAppUsers", // 👉 change as needed
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKey123456")) // 👉 same as in JwtTokenService
        };
    });

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Add Authentication BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
