using Microsoft.Extensions.DependencyInjection;
using SkrøblighedsPakkeLib;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers();
// 👉 Tilføj CORS her
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5500") // din Vue frontend
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

//Database connection string
string connectionString = "Server=mssql17.unoeuro.com;Database=slk2025_dk_db_dt;User Id=slk2025_dk;Password=f49rRH25wcAnbFEthzkB;TrustServerCertificate=True;";


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IPackageRepository, PackageRepository>();
//builder.Services.AddSingleton<ILimitProfileRepository, LimitProfileRepository>();
//builder.Services.AddSingleton<ISensorEventRepository, SensorEventRepository>();

// registrér repositories med connection string
builder.Services.AddScoped<IPackageRepository>(sp => new PackageRepository("Server=mssql17.unoeuro.com;Database=slk2025_dk_db_dt;User Id=slk2025_dk;Password=f49rRH25wcAnbFEthzkB;TrustServerCertificate=True;"));
builder.Services.AddScoped<ILimitProfileRepository>(sp => new LimitProfileRepository("Server=mssql17.unoeuro.com;Database=slk2025_dk_db_dt;User Id=slk2025_dk;Password=f49rRH25wcAnbFEthzkB;TrustServerCertificate=True;"));
builder.Services.AddScoped<ISensorEventRepository>(sp => new SensorEventRepository("Server=mssql17.unoeuro.com;Database=slk2025_dk_db_dt;User Id=slk2025_dk;Password=f49rRH25wcAnbFEthzkB;TrustServerCertificate=True;"));

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:5187"); // backend port

