using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDemo.Data;
using NUnit.Framework;
using NHibernateDemo.Business.Entities;

namespace NHibernateDemo.Test
{
    [TestFixture]
    public class SessionFactoryHelperTest
    {
        [Test]
        public void CreateFactoryTest()
        {
            var factory = new SessionFactoryHelper();
            factory.CreateSessionFactory(true);

            using (var session = factory.CreateSessionFactory().OpenSession())
            {
                session.Save(new CommunicationType() { Value = "Email" });
                session.Save(new CommunicationType() { Value = "Phone" });
            }
        }
    }
}
