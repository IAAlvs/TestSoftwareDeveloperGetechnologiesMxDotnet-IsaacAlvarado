using SalesApi.Models;
namespace SalesApi.Repositories;

public interface IPersonRepository{
    Task<Person?> FindPersonByIdentification(string identificacion);
    Task<List<Person>> FindPersons();
    Task DeletePersonByIdentification(string identificacion);
    Task<Person> StorePerson(Person persona);

}

