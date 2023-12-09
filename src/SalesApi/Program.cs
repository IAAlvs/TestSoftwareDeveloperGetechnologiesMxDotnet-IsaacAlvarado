using FluentValidation;
using FluentValidation.AspNetCore;
using SalesApi.EndpointDefinition;
using SalesApi.Repositories;
using Serilog;

Log.Information("Starting Up");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, loggerConfig) => 
    loggerConfig.WriteTo.Console().ReadFrom.Configuration(context.Configuration));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<IGlobalValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
PersonsEndpointDefinition.DefineServices(builder.Services);
InvoicesEndpointDefinition.DefineServices(builder.Services);

var app = builder.Build();

PersonsEndpointDefinition.DefineEndpoints(app);
InvoicesEndpointDefinition.DefineEndpoints(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
