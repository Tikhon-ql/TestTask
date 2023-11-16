using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Common.Interfaces;
using TestTask.DataProvider.Context;
using TestTask.DataProvider.Interfaces;
using TestTask.DataProvider.Repositories;

namespace TestTask.DataProvider.Configuration
{
    /// <summary>
    /// Static class to exten IServiceColletion to configure dataprovider layer
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Method which add context and repositories to service collection
        /// </summary>
        /// <param name="services">Class, we wanna exten</param>
        /// <param name="configuration">Configuration to get connection string</param>
        public static void ConfigureDataProviderLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStirng = configuration.GetConnectionString("DefaultConnectionString");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connectionStirng);
            });

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
