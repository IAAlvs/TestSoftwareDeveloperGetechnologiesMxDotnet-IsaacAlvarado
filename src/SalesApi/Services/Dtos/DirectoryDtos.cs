namespace SalesApi.Services;
public record AddPersonDto(string Name, string LastName, string? SecondLastName, string Identity);
public record GetPersonDto(Guid Id, string Name, string LastName, string? SecondLastName, string Identity);

