using SalesApi.Repositories;
using SalesApi.Services;
using FluentValidation;
using System.Text.Json.Nodes;
using System.Text.Json;
using SQLitePCL;
using Microsoft.AspNetCore.Mvc;





namespace SalesApi.EndpointDefinition;

public class PersonsEndpointDefinition{
    public static void DefineServices(IServiceCollection services){
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        SQLitePCL.Batteries.Init();
    
        services.AddDbContext<SalesDb>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IDirectoryService, DirectoryService>();
/*         services.AddValidatorsFromAssemblyContaining<IdentifierValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters(); */
    }
    public static void DefineEndpoints(IEndpointRouteBuilder app){
        var API_VERSION = Environment.GetEnvironmentVariable("API_VERSION")??"v1";

        app.MapPost("/api/"+ API_VERSION+"/sales/persons", StorePerson)  
            .WithName("Store person");
        app.MapGet("/api/"+ API_VERSION+"/sales/persons/{identification}", GetPersonById)  
            .WithName("Find person by Id");
        app.MapGet("/api/"+ API_VERSION+"/sales/persons/", GetPersons)  
            .WithName("Get persons");
        app.MapDelete("/api/"+ API_VERSION+"/sales/persons/{identification}", DeletePersonById).
            WithName("Delete person by Id");
    }

    private static async Task<IResult> GetPersons(IDirectoryService directoryService)
    {
        try
        {
            var persons = await directoryService.FindPersons();
            return Results.Ok(persons);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return PrettifyErrorResult(e)!;
        }
    }

    private static async Task<IResult> DeletePersonById(IDirectoryService directoryService, IValidator<string> validator,
        [FromRoute(Name = "identification")] string identificator)
    {
        try
        {
            var validation = validator.Validate(identificator);
            if(!validation.IsValid){
                throw new ValidationException(validation.Errors);
            }
            await directoryService.DeletePersonByIdentification(identificator);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return PrettifyErrorResult(e)!;
        }
    }

    private static async Task<IResult> GetPersonById(IDirectoryService directoryService, IValidator<string> validator,
        [FromRoute(Name = "identification")] string identificator)
    {
        try
        {
            var validation = validator.Validate(identificator);
            if(!validation.IsValid){
                throw new ValidationException(validation.Errors);
            }
            return await directoryService.FindPersonByIdentification(identificator)
                is { } GetPersonDto
                ? Results.Ok(GetPersonDto)
                : Results.NotFound();
        }
        catch (Exception e)
        {
            return PrettifyErrorResult(e)!;
            
        }
    }

    private static async Task<IResult> StorePerson(IDirectoryService directoryService, IValidator<AddPersonDto> validator,
    AddPersonDto dto)
    {
        try
        {   
            var validation = validator.Validate(dto);
            var person = await directoryService.StorePerson(dto);

            return Results.Ok(person);
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
        ArgumentException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        KeyNotFoundException => Results.NotFound(BuildErrorResponseDto(exc.Message)),
        AggregateException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        ApplicationException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        FormatException => Results.Conflict(BuildErrorResponseDto(exc.Message)),
        JsonException => Results.BadRequest(BuildErrorResponseDto("Failed Json Parse. Invalid input pattern")),
        _ => Results.Problem(exc.Message)
    };
}