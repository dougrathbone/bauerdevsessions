using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDemo.Business.Entities;

namespace NHibernateDemo.Data.HibernateMappers
{
    public class UserMap:EntityBaseMap<User>
    {
        public UserMap()
        {
            Map(x => x.UserName);
        }
    }
}
