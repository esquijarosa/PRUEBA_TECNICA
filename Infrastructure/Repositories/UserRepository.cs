using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio de usuarios para la interfaz <see cref="IUserRepository"/>.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Referencia al cliente de acceso a la API REST.
        /// </summary>
        private readonly RestClient _client;
        /// <summary>
        /// Referencia al mapeador entre <see cref="UserDTO"/> y <see cref="UserEntity"/>
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construye una nueva instancia del repositorio de usuarios.
        /// </summary>
        /// <param name="client">Cliente de acceso a la API REST.</param>
        /// <param name="mapper">Mapeador de clases.</param>
        public UserRepository(RestClient client, IMapper mapper, IConfiguration configuration)
        {
            _client = client;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// Obtiene la lista de usuarios registrados en la REST API.
        /// </summary>
        /// <returns><see cref="IEnumerable{UserEntity}"/> con la lista de usuarios registrados en la API REST.</returns>
        public IEnumerable<UserEntity> getAll()
        {
            RestRequest request = new RestRequest(_configuration["apiEndpoints:usersEndpoint"], Method.Get);

            /// Ejecuta de manera asíncrona la petición de los usuarios registrados.
            var response = _client.ExecuteAsync<ApiResponse>(request);
            /// Espera a que termine de ejecutar el método asíncrono.
            response.Wait();

            /// Mapea la lista de usuarios desde <see cref="UserDTO"/> a <see cref="UserEntity"/>.
            return _mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserEntity>>(response.Result.Data.data);
        }
    }
}