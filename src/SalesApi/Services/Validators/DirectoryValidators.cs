using FluentValidation;
using SalesApi.Repositories;
namespace SalesApi.Services;


public class IdentifierValidator :AbstractValidator<string>, IGlobalValidator{

    public IdentifierValidator(){
        RuleFor(x => x).MaximumLength(50).WithMessage("Identification max size is 50");
    }

    private bool BeValidGuid(string guid)
    {
        try{
            bool isValid = Guid.TryParse(guid, out _);
            return isValid;
        }
        catch{
            return false;
        }
        
    }

}


public class AddPersonDtoValidor : AbstractValidator<AddPersonDto>, IGlobalValidator
{
    public AddPersonDtoValidor ()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .MaximumLength(50).WithMessage("Name max length are 50 characters")
            .MinimumLength(3).WithMessage("Name min length are 3 characters");
        RuleFor(x => x.SecondLastName)
            .MaximumLength(50).WithMessage("SecondLastName max length are 50 characters")
            .MinimumLength(3).WithMessage("SecondLastName min length are 3 characters");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName must not be empty")
            .MaximumLength(50).WithMessage("LastName max length are 50 characters")
            .MinimumLength(3).WithMessage("LastName min length are 3 characters");
        RuleFor(x => x.Identity)
            .NotEmpty().WithMessage("IdType must not be empty")
            .MaximumLength(50).WithMessage("IdType max length are 50 characters")
            .MinimumLength(3).WithMessage("IdType min length are 3 characters");



        
    }
}
