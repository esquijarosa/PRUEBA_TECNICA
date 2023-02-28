using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    /// <summary>
    /// Implementación del servicio de usuarios activos para la interfaz <see cref="IActiveUsersService"/>.
    /// </summary>
    public class ActiveUsersService : IActiveUsersService
    {
        /// <summary>
        /// Referencia al repositorio de usuarios.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Construye una nueva instancia de <see cref="ActiveUsersService"/> con una referencia al
        /// repositorio de usuarios.
        /// </summary>
        /// <param name="userRepository">Repositorio de usuarios activos.</param>
        public ActiveUsersService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Obtiene la lista de usuarios activos.
        /// </summary>
        /// <returns><see cref="IEnumerable{UserEntity}"/> con la lista de usuarios activos.</returns>
        public IEnumerable<UserEntity> getActiveUsers()
        {
            /// Obtiene la lista de usuarios desde el repositorio.
            IEnumerable<UserEntity> users = userRepository.getAll();

            List<UserEntity> activeUsers = new List<UserEntity>();

            /// Recorre la lista y valida qué usuarios están activos para agregarlos a la lista
            /// de usuarios activos.
            foreach (UserEntity user in users)
            {
                if (user.status == "active")
                {
                    activeUsers.Add(user);
                }

            }

            /// Regresa la lista de usuarios activos.
            return activeUsers;
        }
    }
}