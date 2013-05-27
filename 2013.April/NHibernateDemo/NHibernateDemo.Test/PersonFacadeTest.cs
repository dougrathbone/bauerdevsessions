using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernateDemo.Data;
using NHibernateDemo.Data.Repositories;
using NHibernateDemo.IntegrationFacade;
using NUnit.Framework;
using NHibernateDemo.Business.Entities;

namespace NHibernateDemo.Test
{
    [TestFixture]
    public class PersonFacadeTest
    {
        User _user;

        [TestFixtureSetUp]
        public void Setup()
        {
            var facade = new UserFacade(new SessionFactoryHelper(), new UserRepository());
            _user = facade.CreateUser(new User() { UserName = "test user" });
        }

        [Test]
        public void CreatePersonTest()
        {
            //setup
            var facade = new PersonFacade(new SessionFactoryHelper(), new PersonRepository(), new CommunicationRepository());
                
            var person = new Person() { Name = "Person One", CreatedBy = _user, UpdatedBy = _user };           

            //act
            person = facade.SavePerson(person);            

            //assert
            Assert.IsNotNull(person);
            Assert.AreNotEqual(0, person.Id);
            Assert.AreEqual("Person One", person.Name);            
        }

        [Test]
        public void AddPersonWithCommsTest()
        {
            //setup
            var facade = new PersonFacade(new SessionFactoryHelper(), new PersonRepository(), new CommunicationRepository());

            var person = new Person() { Name = "Person Two", CreatedBy = _user, UpdatedBy = _user };
            person.Communications.Add(new Communication()
            {
                CommunicationType = new CommunicationType() { Value = "Email" },
                Detail = "simon@here.com",
                CreatedBy = _user,
                UpdatedBy = _user
            });/*
            person.Communications.Add(new Communication()
            {
                CommunicationType = new CommunicationType() { Value = "Phone" },
                Detail = "0299990000",
                CreatedBy = _user,
                UpdatedBy = _user
            });*/

            //act
            person = facade.SavePerson(person);

            //assert
            Assert.IsNotNull(person);
            Assert.AreNotEqual(0, person.Id);
            Assert.AreEqual("Person Two", person.Name);
            Assert.AreEqual("simon@here.com", person.Communications.First().Detail);
        }
    }
}
