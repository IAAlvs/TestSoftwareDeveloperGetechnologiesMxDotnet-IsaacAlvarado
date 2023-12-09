using SalesApi.Services;
namespace SalesApi.Models;

public class Person
{

    public Guid Id { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? SecondLastName { get; set; }

    public string Identification  { get; set; }
    public Person(Guid id, string firstName, string lastName, string? secondLastName, string identification)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        SecondLastName = secondLastName;
        Identification = identification;
    }
    public ICollection<Invoice>? Invoices { get; set; }
    public static Person CreateFromDto(AddPersonDto dto)=> new(Guid.NewGuid(), dto.Name, dto.LastName, dto.SecondLastName, dto.IdType);
    public GetPersonDto ToGetPersonDto() => new(Id, FirstName, LastName, SecondLastName, Identification);

}