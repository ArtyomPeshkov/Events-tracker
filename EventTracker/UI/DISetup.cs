using Autofac;
using Domain;
using System.Reflection;
using SQLDataAccess;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace UI
{
    public class DISetup
    {
        public static Autofac.IContainer GetContainer(Type CustomEventInteractor = null)
        {
            var builder = new ContainerBuilder();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: false)
                .Build();

            if (configuration["Logger"] == "SimpleLogger")
            {
                builder.RegisterType<SimpleLogger>().As<ILogger>().SingleInstance();
            }

            if (configuration["Hasher"] == "SimpleHasher")
            {
                builder.RegisterType<SimpleHasher>().As<IHasher>().SingleInstance();
            }

            var dataAssembly = Assembly.Load("SQLDataAccess");
            var domainAssembly = Assembly.Load("Domain");

            var eventDbContextType = dataAssembly.GetType("SQLDataAccess." + configuration["Context"]);
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                if (configuration["DBType"] == "SQL")
                {
                    optionsBuilder.UseSqlServer(configuration["ConnectionString"]);
                }
                else
                {
                    optionsBuilder.UseInMemoryDatabase("InMemory");
                }

                var dbContextType = dataAssembly.GetType("SQLDataAccess." + configuration["Context"]);

                var constructorInfo = dbContextType.GetConstructor(new[] { typeof(DbContextOptions) });
                if (constructorInfo == null)
                {
                    throw new InvalidOperationException($"No suitable constructor found for {dbContextType.Name}");
                }

                return constructorInfo.Invoke(new object[] { optionsBuilder.Options });
            }).As(eventDbContextType).InstancePerLifetimeScope();

            var idatabaseType = dataAssembly.GetType("SQLDataAccess." + configuration["DatabaseInterface"]);
            var databaseType = dataAssembly.GetType("SQLDataAccess." + configuration["DatabaseImpl"]);
            builder.RegisterType(databaseType).As(idatabaseType).InstancePerLifetimeScope();

            var iCategoryDatabaseInteractor = domainAssembly.GetType("Domain." + configuration["CategoryInterface"]);
            var CategoryInteractor = dataAssembly.GetType("SQLDataAccess." + configuration["CategoryImpl"]);
            builder.RegisterType(CategoryInteractor).As(iCategoryDatabaseInteractor).InstancePerLifetimeScope();

            var iEventDatabaseInteractor = domainAssembly.GetType("Domain." + configuration["EventInterface"]);
            var EventInteractor = dataAssembly.GetType("SQLDataAccess." + configuration["EventImpl"]);
            if (CustomEventInteractor != null)
            {
                builder.RegisterType(CustomEventInteractor).As(iEventDatabaseInteractor).InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType(EventInteractor).As(iEventDatabaseInteractor).InstancePerLifetimeScope();
            }

            var iUserDatabaseInteractor = domainAssembly.GetType("Domain." + configuration["UserInterface"]);
            var UserInteractor = dataAssembly.GetType("SQLDataAccess." + configuration["UserImpl"]);
            builder.RegisterType(UserInteractor).As(iUserDatabaseInteractor).InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
