using FluentNHibernate.Mapping;
using NHibernateDemo.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Data.HibernateMappers
{
    public class CommunicationTypeMap:SubclassMap<CommunicationType>
    {
        public CommunicationTypeMap()
        {
            Extends<Lookup>();
        }
    }
}
