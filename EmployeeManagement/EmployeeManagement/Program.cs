
using EmployeeManagement.CosmosDb;
using EmployeeManagement.ExcelService;
using EmployeeManagement.Interfaces;

using EmployeeManagement.Services;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DbContext
//builder.Services.AddDbContext<MyDbContext>(options =>
 //   options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));


// Register services
builder.Services.AddScoped<IEmployeeBasicDetails, EmployeeBasicDetailsService>();
builder.Services.AddScoped<IEmployeeAdditionalDetails, EmployeeAdditionalDetailsService>();
builder.Services.AddScoped<ICosmosDbService, CosmosDbService>();

// Register Swagger services 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
