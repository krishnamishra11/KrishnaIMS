using IMSRepository.Models;
using IMSRepository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMSRepository.Repository
{
    class PersonRepository:IPersonRepository
    {
        private readonly IMSContext _context;
        

        public PersonRepository(IMSContext context)
        {
            _context = context;
            
        }
        public void Add(Person Person)
        {
            _context.Person.Add(Person);
            _context.SaveChanges();
        }

        public void Edit(Person Person)
        {

            _context.Entry(Person).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public Person FindById(int Id)
        {
            var Person = _context.Person.AsNoTracking().Where(q => q.Id == Id).FirstOrDefault();

            return Person;
        }
        public bool VerifyPerson(Person person)
        {
            return _context.Person.Any(q => q.Name == person.Name && q.Password == person.Password);
        }
        public IEnumerable<Person> FindByName(string Name)
        {
            var Persons = _context.Person.Where(q => q.Name.Contains(Name));

            return Persons;
        }

        public IEnumerable<Person> GetPersons()
        {

            return _context.Person.ToList();
        }

        public void Remove(int Id)
        {
            var Person = _context.Person.Find(Id);
            _context.Person.Remove(Person);
            _context.SaveChangesAsync();
        }

    }
}
