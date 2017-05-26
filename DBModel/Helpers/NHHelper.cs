using DBModel.Models;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;


namespace DBModel.Helpers
{
    public class NHHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                                   
                    configuration.Configure(СonfigHelper.NHBConfigPath);
                    _sessionFactory = Fluently
                        .Configure(configuration)
                        .Mappings(m => m.FluentMappings
                                        .AddFromAssemblyOf<User>())
                       // .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))
                        .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
