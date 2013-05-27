using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Business.Entities
{
    public abstract class ManagedEntity : EntityBase
    {
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
    }
}
