using BankSystem7.Services.Configuration;
using BankSystem7.Services;
using Microsoft.EntityFrameworkCore;
using TrainManagementSystem.Extensions;
using TrainManagementSystem.Services.Configuration;
using TrainManagementSystem.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Serialization;

const string DatabaseName = "Test2";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(GetNewtonsoftJsonOptions());

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

Action<JsonOptions> GetJsonOptions()
{
    return delegate(JsonOptions jsonOptions)
    {
        jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        jsonOptions.JsonSerializerOptions.WriteIndented = true;
    };
}

Action<MvcNewtonsoftJsonOptions> GetNewtonsoftJsonOptions()
{
    return delegate(MvcNewtonsoftJsonOptions jsonOptions)
    {
        jsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        jsonOptions.SerializerSettings.Formatting = Formatting.None;
        
        Console.WriteLine(jsonOptions.SerializerSettings.Formatting);
    };
}

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