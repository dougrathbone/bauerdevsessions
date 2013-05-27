using NHibernateDemo.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace NHibernateDemo.Data.HibernateMappers
{
    public class LookupMap : EntityBaseMap<Lookup>
    {
        public LookupMap()
        {
            Map(x => x.Value);
        }
    }
}
