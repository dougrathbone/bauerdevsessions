using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Data.Repositories
{
    public interface IBaseRepository<T> {
        void Save(T entity, ISession session);
    }

    public class BaseRepository<T>:IBaseRepository<T>
    {
        public void Save(T entity, ISession session)
        {
            session.SaveOrUpdate(entity);
        }
    }
}
