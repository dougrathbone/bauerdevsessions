using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernateDemo.Framework;
using System.Configuration;

namespace NHibernateDemo.Data
{
    public class SessionFactoryHelper : ISessionFactoryHelper
    {    
        public NHibernate.ISessionFactory CreateSessionFactory(bool createDb=false)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConfigurationManager.ConnectionStrings["nhibernate"].ConnectionString))
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<HibernateMappers.PersonMap>();
                    m.HbmMappings.AddFromAssemblyOf<HibernateMappers.PersonMap>();
                    m.MergeMappings();
                })
                .ExposeConfiguration(x => new SchemaExport(x).Create(false, createDb))
                .BuildSessionFactory();
        }
    }
}
