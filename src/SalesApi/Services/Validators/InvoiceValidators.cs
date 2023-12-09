using FluentValidation;
using SalesApi.Repositories;
namespace SalesApi.Services;


public class GuidValidator :AbstractValidator<Guid>, IGlobalValidator{

    public GuidValidator(){
        RuleFor(x => x.ToString()).Must(BeValidGuid).WithMessage("Param must be a valid UUID");
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


public class AddInvoiceDtoValidor : AbstractValidator<AddInvoiceDto>, IGlobalValidator
{
    public AddInvoiceDtoValidor ()
    {
        RuleFor(x => x.PersonId.ToString()).Must(x=>BeValidGuid(x)).
            WithMessage("Name must not be empty");
        RuleFor(x => x.Date.ToString()).Must(BeAValidDate).WithMessage("must have a valid datetime format");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("must be greater than 0");

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
    private bool BeAValidDate(string value)
    {
        DateTime date;
        return DateTime.TryParse(value, out date);
    }
}
