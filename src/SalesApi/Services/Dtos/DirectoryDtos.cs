namespace SalesApi.Services;
public record AddPersonDto(string Name, string LastName, string? SecondLastName, string IdType);
public record GetPersonDto(Guid Id, string Name, string LastName, string? SecondLastName, string IdType);

