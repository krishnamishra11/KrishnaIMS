using IMSRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMSRepository.Repository.Interfaces
{
    public interface IPersonRepository
    {

        public void Add(Person person);
        public void Edit(Person person);
        public void Remove(int Id);
        public  IEnumerable<Person> GetPersons();
        public bool VerifyPerson(Person person);
        public Person FindById(int Id);
        public IEnumerable<Person> FindByName(string Name);
    }
}
