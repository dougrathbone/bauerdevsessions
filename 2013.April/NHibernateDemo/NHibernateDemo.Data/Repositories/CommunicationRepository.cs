using NHibernateDemo.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Data.Repositories
{
    public interface ICommunicationRepository : IBaseRepository<Communication>
    {
        
    }

    public class CommunicationRepository:BaseRepository<Communication>, ICommunicationRepository
    {
    }
}
