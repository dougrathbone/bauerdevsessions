using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Business.Entities
{
    public class User:EntityBase
    {
        public virtual string UserName { get; set; }
    }
}
