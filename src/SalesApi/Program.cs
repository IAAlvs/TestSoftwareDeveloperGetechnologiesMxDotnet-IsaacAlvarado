using FluentValidation;
using FluentValidation.AspNetCore;
using SalesApi.AspectDefinitions;
using SalesApi.EndpointDefinition;
using SalesApi.Repositories;
using Serilog;

Log.Information("Starting Up");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, loggerConfig) => 
    loggerConfig.WriteTo.Console().ReadFrom.Configuration(context.Configuration));
    
CorsAspectDefinition.DefineAspect(builder.Services, builder.Configuration);
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
CorsAspectDefinition.ConfigureAspect(app);

PersonsEndpointDefinition.DefineEndpoints(app);
InvoicesEndpointDefinition.DefineEndpoints(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{       const string apiVersion = "v1";
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/"+apiVersion+"/sales/swagger/{documentname}/swagger.json";
        });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"{apiVersion}/swagger.json", "Sales test api");
            options.RoutePrefix = $"api/{apiVersion}/sales/swagger";
        });
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
