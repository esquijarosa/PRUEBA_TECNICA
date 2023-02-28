using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio de usuarios para la interfaz <see cref="IUserRepository"/>.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly RestClient _client;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IUserRepository> _logger;

        /// <summary>
        /// Construye una nueva instancia del repositorio de usuarios.
        /// </summary>
        /// <param name="client">Cliente de acceso a la API REST.</param>
        /// <param name="mapper">Mapeador de clases.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        /// <param name="logger">Logging infrastructure.</param>
        public UserRepository(RestClient client, IMapper mapper, IConfiguration configuration, ILogger<IUserRepository> logger)
        {
            _client = client;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la lista de usuarios registrados en la REST API.
        /// </summary>
        /// <returns><see cref="IEnumerable{UserEntity}"/> con la lista de usuarios registrados en la API REST.</returns>
        public IEnumerable<UserEntity> GetAll()
        {
            try
            {
                RestRequest request = new RestRequest(_configuration["apiEndpoints:usersEndpoint"], Method.Get);

                var response = _client.ExecuteAsync<ApiResponse>(request);
                response.Wait();

                if (response.Result.IsSuccessful)
                {
                    return _mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserEntity>>(response.Result.Data.data);
                }
                else
                    return new List<UserEntity>();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}