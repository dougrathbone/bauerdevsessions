using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDemo.Business.Entities;
using NHibernate;
using NHibernate.Linq;

namespace NHibernateDemo.Data.Repositories
{
    public interface IPersonRepository :IBaseRepository<Person>
    {
        Person FetchPerson(string name, ISession session);
        Person FetchPerson(int id, ISession session);
    }

    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public Person FetchPerson(string name, ISession session)
        {
            return session.Query<Person>().Where(x => x.Name == name).FirstOrDefault();
        }

        public Person FetchPerson(int id, ISession session)
        {
            return session.Get<Person>(id);
        }
    }
}
