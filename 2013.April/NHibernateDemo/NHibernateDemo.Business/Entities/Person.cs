using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Business.Entities
{
    public class Person:ManagedEntity
    {
        public Person()
        {
            Communications = new List<Communication>();
        }

        public virtual string Name { get; set; }
        public virtual IList<Communication> Communications { get; set; }
    }
}
