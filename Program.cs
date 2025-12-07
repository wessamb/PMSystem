using System.Text;
using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PMSystem.ActionFilter;
using PMSystem.Application.Interface;
using PMSystem.Application.Mappings;
using PMSystem.Application.Services;
using PMSystem.Application.Settings;
using PMSystem.Application.Validators;
using PMSystem.Domain.Entities;
using PMSystem.Infrastructure;
using PMSystem.Middlewares;
using Scalar.AspNetCore;
using Scrutor;
using Serilog;

// ÇáßæÏ íÈÏÃ ãÈÇÔÑÉ åäÇ ÈÏæä ÝÆÉ Ãæ ÏÇáÉ Main
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
options.AddPolicy("Policy1",
    policy =>
    {
        policy.WithOrigins("https://localhost:7177/scalar/v1"
                            );
    });

options.AddPolicy("AnotherPolicy",
    policy =>
    {
        policy.WithOrigins("https://localhost:7177/scalar/v1")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
    });
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<PMSystemDbContext>().AddDefaultTokenProviders();
builder.Services.Scan(scan=> scan
    .FromAssemblyOf<IProjectService>()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
    
    .AsImplementedInterfaces()
    .WithScopedLifetime());
builder.Services.AddProblemDetails();
builder.Services.AddScoped<CustomResourceFilter>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<PMSystemDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option =>
{
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();
// Remove or comment out this line, as it's not available
var app = builder.Build();
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
    app.MapScalarApiReference();
//}
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCustomMiddleware();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }