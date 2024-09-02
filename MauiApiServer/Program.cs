using MauiApiServer.Data.Core;
using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Infrastructure.DataExctractors;
using MauiApiServer.Data.Infrastructure.DataExctractors.ExcelExctractors;
using MauiApiServer.Data.Infrastructure.DataExctractors.Interfaces;
using MauiApiServer.Data.Infrastructure.DataParsing;
using MauiApiServer.Data.Infrastructure.DataParsing.Interfaces;
using MauiApiServer.Data.Infrastructure.DataParsing.Parsers;
using MauiApiServer.Data.Infrastructure.Validators;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// My services
builder.Services.AddTransient<IDataService, DataService>();

builder.Services.AddTransient<IDataExtractor, DataExtractor>();
builder.Services.AddTransient<IDataValidator, DataValidator>();
builder.Services.AddTransient<IDataParser, DataParser>();

builder.Services.AddTransient<IExcelDataReader, ExcelDataReader>();
builder.Services.AddTransient<IPersonParser, PersonParser>();
builder.Services.AddTransient<IDateParser, DateParser>();

//builder.Services.AddTransient<Func<string, bool>>(provider =>
//{
    
//    return str => bool.Parse(str);
//});

// My Db Context
builder.Services.AddDbContext<AppDbContext>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // Set limit to 100 MB
});


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
