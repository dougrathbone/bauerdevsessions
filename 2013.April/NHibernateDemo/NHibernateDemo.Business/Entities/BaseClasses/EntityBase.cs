using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Business.Entities
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
    }
}
