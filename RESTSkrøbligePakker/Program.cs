using Microsoft.Extensions.DependencyInjection;
using SkrøblighedsPakkeLib;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Database connection string
string connectionString = "Server=mssql17.unoeuro.com;Database=slk2025_dk_db_dt;User Id=slk2025_dk;Password=f49rRH25wcAnbFEthzkB;TrustServerCertificate=True;";


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPackageRepository, PackageRepository>();
builder.Services.AddSingleton<ILimitProfileRepository, LimitProfileRepository>();
builder.Services.AddSingleton<ISensorEventRepository, SensorEventRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
