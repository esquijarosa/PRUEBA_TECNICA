using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PRUEBA_TECNICA
{
    class Program
    {
        /// <summary>
        /// Punto de entrada de la aplicación de consola.
        /// </summary>
        /// <param name="args">No se utilizan</param>
        static void Main(string[] args)
        {
            #region Configuración de la injección de dependencias

            var services = new ServiceCollection();
            configureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            #endregion

            /// Crea una instancia del servicio de usuarios activos.
            IActiveUsersService userService = serviceProvider.GetService<IActiveUsersService>();

            /// Obtiene la lista de usuarios activos.
            IEnumerable<UserEntity> users = userService.getActiveUsers();
            /// Escribe en la consola un mensaje con el número de usuarios activos obtenidos.
            Console.WriteLine($"Usuarios: {users.Count()}");

            /// Crea una instancia del servicio de almacenamiento en disco de las imágenes
            /// de los usuarios activos.
            IGravatarToDiskService toDiskService = serviceProvider.GetService<IGravatarToDiskService>();
            
            /// Almacena en disco las imágenes de los usuarios activos.
            toDiskService.saveGravatarFromUsers(users);
        }

        /// <summary>
        /// Configura la injección de dependencias del proyecto.
        /// </summary>
        /// <param name="services">Referencia a l colección de servicios.</param>
        private static void configureServices(IServiceCollection services)
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
