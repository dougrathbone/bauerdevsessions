using NHibernateDemo.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Data.HibernateMappers
{
    public abstract class ManagedEntityMap<T>: EntityBaseMap<T>
        where T:ManagedEntity
    {
        public ManagedEntityMap()
        {
            References(x => x.CreatedBy).Not.Nullable();
            References(x => x.UpdatedBy).Not.Nullable();
            
            Map(x => x.CreatedDate).Not.Nullable().Default("getdate()").Generated.Always();
            Map(x => x.UpdatedDate).Not.Nullable().Default("getdate()").Generated.Insert();
        }
    }
}
