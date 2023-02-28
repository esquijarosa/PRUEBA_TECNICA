using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.DTO;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        /// <summary>
        /// Punto de entrada de la aplicación de consola.
        /// </summary>
        /// <param name="args">No se utilizan.</param>
        static void Main(string[] args)
        {
            _configuration = BuildConfiguration(new ConfigurationBuilder());

            #region Configuración de la injección de dependencias

            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            #endregion

            /// Crea una instancia del servicio de usuarios activos.
            IActiveUsersService userService = serviceProvider.GetService<IActiveUsersService>();

            /// Obtiene la lista de usuarios activos.
            IEnumerable<UserEntity> users = userService.GetActiveUsers();
            /// Escribe en la consola un mensaje con el número de usuarios activos obtenidos.
            Console.WriteLine($"Usuarios: {users.Count()}");

            /// Crea una instancia del servicio de almacenamiento en disco de las imágenes
            /// de los usuarios activos.
            IGravatarToDiskService toDiskService = serviceProvider.GetService<IGravatarToDiskService>();

            /// Almacena en disco las imágenes de los usuarios activos.
            toDiskService.SaveGravatarFromUsers(users);
        }

        /// <summary>
        /// Configura la injección de dependencias del proyecto.
        /// </summary>
        /// <param name="services">Referencia a l colección de servicios.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            /// Configura una instancia (Singleton) única del cliente de RestSharp
            /// para la comunicación con REST APIs
            services.AddSingleton<RestClient, RestClient>();
            /// Condigura el repositorio de usuarios, enlazando la interfaz con la clase concreta.
            /// Se crea una instancia nueva cada vez que se requiere el servicio (Transient)
            services.AddTransient<IUserRepository, UserRepository>();
            /// Configura el servicio de usuarios activos, enlazando la interfaz con la clase concreta.
            services.AddTransient<IActiveUsersService, ActiveUsersService>();
            /// Configura el repositorio de imágenes de usuario.
            services.AddTransient<IGravatarRepository, GravatarRepository>();
            /// Configura el servicio de grabado a disco de las imágenes de usuario.
            services.AddTransient<IGravatarToDiskService, GravatarToDiskService>();

            /// Configura el servicio de AutoMapper para la conversión del <see cref="UserDTO"/>
            /// recibido desde la API en <see cref="UserEntity"/>.
            services.AddAutoMapper(typeof(UserMapProfile));

            /// Configura el servicio de configuración para la aplicación.
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