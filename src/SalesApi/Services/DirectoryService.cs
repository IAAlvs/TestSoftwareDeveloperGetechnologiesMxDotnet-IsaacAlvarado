using SalesApi.Models;
using SalesApi.Repositories;
namespace SalesApi.Services;

public class DirectoryService : IDirectoryService
{
    private readonly IPersonRepository _personRepository;
    public DirectoryService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    public async Task DeletePersonByIdentification(string identificacion)
    {
        await _personRepository.DeletePersonByIdentification(identificacion);
    }

    public async Task<List<GetPersonDto>> FindPersons()
    {
        var persons = await _personRepository.FindPersons();
        return persons.Select(p => p.ToGetPersonDto()).ToList();
    }

    public async Task<GetPersonDto?> FindPersonByIdentification(string identificacion)
    {
        var person = await _personRepository.FindPersonByIdentification(identificacion);
        if(person != null)
            return person.ToGetPersonDto();
        return null;
        
    }
    public async Task<GetPersonDto> StorePerson(AddPersonDto person)
    {
        var personSaved = await _personRepository.StorePerson(Person.CreateFromDto(person));
        return personSaved.ToGetPersonDto();
    }
}