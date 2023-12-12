
using System.Text.Json;
using FluentValidation;
using SalesApi.Repositories;
using SalesApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace SalesApi.EndpointDefinition;

public class InvoicesEndpointDefinition{
    public static void DefineServices(IServiceCollection services){
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        SQLitePCL.Batteries.Init();
        services.AddDbContext<SalesDb>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceService, InvoiceService>();

    }
    public static void DefineEndpoints(IEndpointRouteBuilder app){
        var API_VERSION = Environment.GetEnvironmentVariable("API_VERSION")??"v1";
        app.MapPost("/api/"+ API_VERSION+"/sales/invoices/persons", AddInvoice)  
            .WithName("Add invoice");
        app.MapGet("/api/"+ API_VERSION+"/sales/invoices/persons/{personId}", GetInvoicesByPerson)  
            .WithName("Get invoices from person");
    }

    private async static Task<IResult> GetInvoicesByPerson(IInvoiceService invoiceService, IValidator<Guid> validator, [FromRoute(Name = "personId")]  Guid personId)
    {
        try
        {
            var invoices = await invoiceService.GetInvoicesByPersonId(personId);
            return Results.Ok(invoices);
        }
        catch (Exception e)
        {
            return PrettifyErrorResult(e)!;
        }
    }

    private async static Task<IResult>  AddInvoice(IInvoiceService invoiceService, IValidator<AddInvoiceDto> validator,
    AddInvoiceDto addInvoiceRequestDto)
    {
        try
        {
            var validation = validator.Validate(addInvoiceRequestDto);
            if(!validation.IsValid){
                throw new ValidationException(validation.Errors);
            }
            var invoices = await invoiceService.AddInvoice(addInvoiceRequestDto);
            return Results.Ok(invoices);
        }
        catch (Exception e)
        {
            return PrettifyErrorResult(e)!;
        }
    }
    private static List<string>  BuildErrorResponseDto(string errorMessage)
    {
        return new List<string> (new List<string> { errorMessage });
    } 
    private static IResult? PrettifyErrorResult(Exception exc) => exc switch
    {
        ValidationException ex => Results.UnprocessableEntity(new { errors = ex.Errors.Select(x => $"{x.PropertyName} {x.ErrorMessage}") }),
        InvalidOperationException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        ArgumentNullException => Results.NotFound(BuildErrorResponseDto(exc.Message)),
        ArgumentException => Results.UnprocessableEntity(BuildErrorResponseDto(exc.Message)),
        KeyNotFoundException => Results.NotFound(BuildErrorResponseDto(exc.Message)),
        AggregateException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        ApplicationException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        FormatException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        JsonException => Results.BadRequest(BuildErrorResponseDto("Failed Json Parse. Invalid input pattern")),
        _ => Results.Problem(exc.Message)
    };
}