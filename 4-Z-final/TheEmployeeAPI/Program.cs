using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Testcontainers.PostgreSql;

var postgreSqlContainer = new PostgreSqlBuilder().Build();
await postgreSqlContainer.StartAsync();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TheEmployeeAPI.xml"));
});
builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options => 
{
    options.Filters.Add<FluentValidationFilter>();
});
builder.Services.AddSingleton<ISystemClock, SystemClock>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var conn = postgreSqlContainer.GetConnectionString();
    options.UseNpgsql(conn);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Tokens:Issuer"],
                ValidAudience = configuration["Tokens:Issuer"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]!))
            };

            options.TokenValidationParameters = tokenValidationParameters;
        });


var app = builder.Build();

//kill container on shutdown
app.Lifetime.ApplicationStopping.Register(() => postgreSqlContainer.DisposeAsync());

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.MigrateAndSeed(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program {}