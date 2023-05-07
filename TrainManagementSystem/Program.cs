using BankSystem7.Services.Configuration;
using BankSystem7.Services;
using Microsoft.EntityFrameworkCore;
using TrainManagementSystem.Extensions;
using TrainManagementSystem.Services.Configuration;
using TrainManagementSystem.Data;

const string DatabaseName = "Test2";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTrainManagementSystem(GetOptions());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

ConfigurationOptions GetOptions()
{
    return new ConfigurationOptions
    {
        EnsureCreated = false,
        EnsureDeleted = false,
        DatabaseName = DatabaseName,
        OperationOptions = new OperationServiceOptions
        {
            DatabaseName = DatabaseName,
        },
        Contexts = new Dictionary<DbContext, ModelConfiguration?>
        {
            { new TrainContext(), new TrainManagementModelConfiguration() },
        }
    };
}