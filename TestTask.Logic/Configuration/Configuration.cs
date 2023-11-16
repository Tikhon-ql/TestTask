using Microsoft.Extensions.DependencyInjection;
using TestTask.Logic.Interfaces;
using TestTask.Logic.Services;

namespace TestTask.Logic.Configuration
{
    /// <summary>
    /// Static class to exten IServiceColletion to configure logic layer
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Method which adds services to service collection
        /// </summary>
        /// <param name="services">Class, we wanna exten</param>
        public static void ConfigureLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();
        }
    }
}
