using Microsoft.EntityFrameworkCore;
using SalesApi.Models;
namespace SalesApi.Repositories;



public class PersonRepository : IPersonRepository
{
    private readonly SalesDb _db;
    public PersonRepository(SalesDb db) => _db = db;

    public async Task DeletePersonByIdentification(string personaId)
    {
        var person = await _db.Persons.FirstOrDefaultAsync(person => person.Identification == personaId);
        if (person != null){
            _db.Persons.Remove(person);
            await _db.SaveChangesAsync();
            return;
        }
        throw new KeyNotFoundException($"Not personId founded for identification : {personaId}");
    }

    async public Task<List<Person>> FindPersons()
    {
        return await _db.Persons.ToListAsync();
    }

    public async Task<Person?> FindPersonByIdentification(string identificacion)
    {
        return await _db.Persons.FirstOrDefaultAsync(p => p.Identification == identificacion);
    }

    public async Task<Person> StorePerson(Person person)
    {
        var personWithId = await _db.Persons.FirstOrDefaultAsync(p => p.Identification == person.Identification);
        if(personWithId != null)
            throw new ArgumentException($"Person with identification {person.Identification} already exists ");
        var addedPerson = await _db.Persons.AddAsync(person);
        await _db.SaveChangesAsync();
        return addedPerson.Entity;
    }

}