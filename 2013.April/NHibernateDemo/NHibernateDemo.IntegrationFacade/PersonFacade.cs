using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernateDemo.Business.Entities;
using NHibernateDemo.Data.Repositories;
using NHibernateDemo.Framework;
using NHibernate.Linq;

namespace NHibernateDemo.IntegrationFacade
{
    public class PersonFacade
    {
        private readonly ISessionFactoryHelper _sessionFactoryHelper;
        private readonly ISessionFactory _sessionFactory;
        private readonly IPersonRepository _personRepository;
        private readonly ICommunicationRepository _communicationRepository;

        public PersonFacade(ISessionFactoryHelper sessionFactoryHelper, IPersonRepository personRepository, ICommunicationRepository communicationRepository) 
        {
            _sessionFactoryHelper = sessionFactoryHelper;
            _sessionFactory = _sessionFactoryHelper.CreateSessionFactory();
            _personRepository = personRepository;
            _communicationRepository = communicationRepository;
        }

        public Person SavePerson(Person person)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var existing = _personRepository.FetchPerson(person.Name, session);
                    var personToSave = existing == null ? new Person() { Name = person.Name, UpdatedBy = person.UpdatedBy, CreatedBy = person.CreatedBy } : existing;                    
                    
                    foreach (var item in person.Communications)
                    {
                        item.CommunicationType = session.Query<CommunicationType>().Where(x => x.Value == item.CommunicationType.Value).First();
                        personToSave.Communications.Add(item);
                    }
                    _personRepository.Save(personToSave, session);                    
                    transaction.Commit();
                    return personToSave;
                }
            }
        }
    }
}
