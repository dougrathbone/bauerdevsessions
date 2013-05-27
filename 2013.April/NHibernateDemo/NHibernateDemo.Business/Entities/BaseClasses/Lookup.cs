using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Business.Entities
{
    public abstract class Lookup : EntityBase
    {
        public virtual string Value { get; set; }
    }
}
