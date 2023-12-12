using FluentValidation;
using FluentValidation.AspNetCore;
using SalesApi.Repositories;

namespace SalesApi.AspectDefinitions;


public class ValidationAspectDefinition
{

    public static void DefineAspect(IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssemblyContaining<IGlobalValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
    }

    public static void ConfigureAspect(WebApplication app)
    {
        throw new NotImplementedException();
    }
}