using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDemo.Business.Entities;

namespace NHibernateDemo.Data.HibernateMappers
{
    public class PersonMap:ManagedEntityMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            HasManyToMany<Communication>(x => x.Communications).Table("PersonCommunication").Cascade.SaveUpdate();
            //HasMany(x => x.Communications);
        }
    }
}
