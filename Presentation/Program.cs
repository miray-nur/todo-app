using Business;
using Business.Configurations;
using DataAccess;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

builder.Services.AddDbContext<MasterContext>(options =>
{
    options.UseNpgsql(connectionString);
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
});

//Audit.Net kullanýmý için aktif edilmelidir.
//Audit.Core.Configuration.DataProvider = new PostgreSqlDataProvider(config =>
//    config
//        .ConnectionString(connectionString)
//        .TableName("AuditLogs")
//        .DataColumn("Data")
//        .IdColumnName("Id")
//        );


//DataAccess
builder.Services.AddDataAccessDependencies();

//Business
builder.Services.AddBusinessDependecies(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IQueueService>().Consumer();
}

await app.Services.CreateScope().ServiceProvider.GetRequiredService<MasterContext>().Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
