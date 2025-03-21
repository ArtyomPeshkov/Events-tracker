using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Reflection;
using SQLDataAccess;

//Логгер - Singleton - через свойство
//eventHangler - Scoped - через конструктор
//hasher - Transient - через метод
namespace UI
{
    public class DISetup
    {
        public static ServiceProvider GetProvider()
        {
            var services = new ServiceCollection();

            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("di-config.json", optional: false)
            //    .Build();
            //services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<ILogger, SimpleLogger>();
            services.AddTransient<IHasher, SimpleHasher>();

            var dataAssembly = Assembly.Load("SQLDataAccess");
            var domainAssembly = Assembly.Load("Domain");

            var idatabaseType = dataAssembly.GetType("SQLDataAccess.IDatabase");
            var databaseType = dataAssembly.GetType("SQLDataAccess.Database");
            services.AddSingleton(idatabaseType, databaseType);

            var iCategoryDatabaseInteractor = domainAssembly.GetType("Domain.ICategoryDatabaseInteractor");
            var CategoryInteractor = dataAssembly.GetType("SQLDataAccess.CategoryDatabaseInteractor");
            services.AddScoped(iCategoryDatabaseInteractor, CategoryInteractor);

            var iEventDatabaseInteractor = domainAssembly.GetType("Domain.IEventDatabaseInteractor");
            var EventInteractor = dataAssembly.GetType("SQLDataAccess.EventDatabaseInteractor");
            services.AddScoped(iEventDatabaseInteractor, EventInteractor);

            var iUserDatabaseInteractor = domainAssembly.GetType("Domain.IUserDatabaseInteractor");
            var UserInteractor = dataAssembly.GetType("SQLDataAccess.UserDatabaseInteractor");
            services.AddScoped(iUserDatabaseInteractor, UserInteractor);


            return services.BuildServiceProvider();
        }
    }
}
