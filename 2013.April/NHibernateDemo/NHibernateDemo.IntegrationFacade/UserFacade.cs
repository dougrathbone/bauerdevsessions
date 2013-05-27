using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernateDemo.Business.Entities;
using NHibernateDemo.Data.Repositories;
using NHibernateDemo.Framework;

namespace NHibernateDemo.IntegrationFacade
{
    public class UserFacade
    {
        private readonly ISessionFactoryHelper _sessionFactoryHelper;
        private readonly ISessionFactory _sessionFactory;
        private readonly IUserRepository _userRepository;

        public UserFacade(ISessionFactoryHelper sessionFactoryHelper, IUserRepository userRepository) 
        {
            _sessionFactoryHelper = sessionFactoryHelper;
            _sessionFactory = _sessionFactoryHelper.CreateSessionFactory();
            _userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var existing = _userRepository.FetchUser(user.UserName,session);
                if (existing != null)
                {
                    return existing;
                }
                _userRepository.Save(user, session);
                return _userRepository.FetchUser(user.UserName, session);
            }
        }
    }
}
