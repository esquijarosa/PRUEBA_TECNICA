using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace PRUEBA_TECNICA
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            configureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            IActiveUsersService userService = serviceProvider.GetService<IActiveUsersService>();

            IEnumerable<UserEntity> users = userService.getActiveUsers();
            Console.WriteLine($"Usuarios: {users.Count()}");

            IGravatarToDiskService toDiskService = serviceProvider.GetService<IGravatarToDiskService>();
            
            toDiskService.saveGravatarFromUsers(users);
        }

        private static void configureServices(IServiceCollection services)
        {
            services.AddSingleton<RestClient, RestClient>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IActiveUsersService, ActiveUsersService>();
            services.AddTransient<IGravatarRepository, GravatarRepository>();
            services.AddTransient<IGravatarToDiskService, GravatarToDiskService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
