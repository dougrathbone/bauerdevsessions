using FluentNHibernate.Mapping;
using NHibernateDemo.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Data.HibernateMappers
{
    public abstract class EntityBaseMap<T>:ClassMap<T>
        where T:EntityBase
    {
        public EntityBaseMap()
        {
            Id(x => x.Id);
        }
    }
}
