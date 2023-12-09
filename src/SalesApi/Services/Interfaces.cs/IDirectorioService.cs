
namespace SalesApi.Services;

public interface IDirectoryService{
    Task<GetPersonDto?> FindPersonByIdentification(string identification);
    Task<List<GetPersonDto>> FindPersons();

    Task DeletePersonByIdentification(string identification);
    Task<GetPersonDto> StorePerson(AddPersonDto person);

}