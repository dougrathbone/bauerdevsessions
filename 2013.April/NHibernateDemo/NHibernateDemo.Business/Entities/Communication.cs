using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Business.Entities
{
    public class Communication:ManagedEntity
    {
        public virtual CommunicationType CommunicationType { get; set; }
        public virtual string Detail { get; set; }
    }
}
