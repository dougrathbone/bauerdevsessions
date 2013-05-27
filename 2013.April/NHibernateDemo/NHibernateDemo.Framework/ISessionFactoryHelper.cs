using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Framework
{
    public interface ISessionFactoryHelper
    {
        NHibernate.ISessionFactory CreateSessionFactory(bool createDb = false);
    }
}
