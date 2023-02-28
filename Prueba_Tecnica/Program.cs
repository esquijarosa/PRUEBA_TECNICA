using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.DTO;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PRUEBA_TECNICA
{
    class Program
    {
        private static IConfiguration _configuration;
        private static ILogger<Program> _logger;

        /// <summary>
        /// Punto de entrada de la aplicación de consola.
        /// </summary>
        /// <param name="args">No se utilizan.</param>
        static void Main(string[] args)
        {
            try
            {
                _configuration = BuildConfiguration(new ConfigurationBuilder());

                var services = new ServiceCollection();
                ConfigureServices(services);

                var serviceProvider = services.BuildServiceProvider();

                _logger = serviceProvider.GetService<ILogger<Program>>();

                IActiveUsersService userService = serviceProvider.GetService<IActiveUsersService>();

                IEnumerable<UserEntity> users = userService.GetActiveUsers();
                Console.WriteLine($"Usuarios: {users.Count()}");

                IGravatarToDiskService toDiskService = serviceProvider.GetService<IGravatarToDiskService>();

                toDiskService.SaveGravatarFromUsers(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Configura la injección de dependencias del proyecto.
        /// </summary>
        /// <param name="services">Referencia a l colección de servicios.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<RestClient, RestClient>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IActiveUsersService, ActiveUsersService>();
            services.AddTransient<IGravatarRepository, GravatarRepository>();
            services.AddTransient<IGravatarToDiskService, GravatarToDiskService>();

            services.AddAutoMapper(typeof(UserMapProfile));

            services.AddSingleton(Program._configuration);

            services.AddLogging();
        }

        /// <summary>
        /// Enlaza el archivo de configuración para la aplicación.
        /// </summary>
        /// <param name="builder">Constructor de configuración.</param>
        /// <returns><see cref="IConfiguration"/> con la configuración de la aplicación.</returns>
        private static IConfiguration BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}