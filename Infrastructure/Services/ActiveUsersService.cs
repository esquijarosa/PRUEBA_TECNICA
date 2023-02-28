using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    /// <summary>
    /// Implementación del servicio de usuarios activos para la interfaz <see cref="IActiveUsersService"/>.
    /// </summary>
    public class ActiveUsersService : IActiveUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<IActiveUsersService> _logger;

        /// <summary>
        /// Construye una nueva instancia de <see cref="ActiveUsersService"/> con una referencia al
        /// repositorio de usuarios.
        /// </summary>
        /// <param name="userRepository">Repositorio de usuarios activos.</param>
        /// <param name="logger">Logging infrastructure.</param>
        public ActiveUsersService(IUserRepository userRepository, ILogger<IActiveUsersService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la lista de usuarios activos.
        /// </summary>
        /// <returns><see cref="IEnumerable{UserEntity}"/> con la lista de usuarios activos.</returns>
        public IEnumerable<UserEntity> GetActiveUsers()
        {
            try
            {
                IEnumerable<UserEntity> users = _userRepository.GetAll();

                List<UserEntity> activeUsers = new List<UserEntity>();

                foreach (UserEntity user in users)
                {
                    if (user.status == "active")
                    {
                        activeUsers.Add(user);
                    }
                }

                return activeUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}