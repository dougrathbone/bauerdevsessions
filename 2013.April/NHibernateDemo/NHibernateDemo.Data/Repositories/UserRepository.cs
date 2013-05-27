using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDemo.Business.Entities;
using NHibernate;

namespace NHibernateDemo.Data.Repositories
{
    public interface IUserRepository :IBaseRepository<User>
    {
        User FetchUser(string name, ISession session);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User FetchUser(string name, ISession session)
        {
            return session.QueryOver<User>().Where(x => x.UserName == name).List().FirstOrDefault();
        }
    }
}
