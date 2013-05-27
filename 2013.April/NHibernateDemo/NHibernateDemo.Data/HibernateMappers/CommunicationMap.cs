using NHibernateDemo.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Data.HibernateMappers
{
    class CommunicationMap: ManagedEntityMap<Communication>
    {
        public CommunicationMap()
        {
            References(x => x.CommunicationType).Not.Nullable().Not.LazyLoad();
            Map(x => x.Detail).Not.Nullable();            
        }
    }
}
