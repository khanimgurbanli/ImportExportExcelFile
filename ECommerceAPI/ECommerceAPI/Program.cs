using ECommerceAPI.Domain;
using ECommerceAPI.Presentation;
using Microsoft.AspNetCore.Http.Features;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//----For EntityFramework connection configuration file----
builder.Services.AddPersistenceServices();

var path = Directory.GetCurrentDirectory();


builder.Services.AddControllers()
                       .AddJsonOptions(opts =>
                       {
                           opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                       });


////Logger  Configure ---> vaxtim catmadi(

//var sqlPath = Environment.CurrentDirectory + @"/WebApp.db";
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.SQLite(sqliteDbPath: sqlPath, tableName: "Log", batchSize: 1)
//    .CreateLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
