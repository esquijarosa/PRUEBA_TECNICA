using AutoMapper;
using RestSharp;
using System.Collections.Generic;

/// <summary>
/// Implementación del repositorio de usuarios para la interfaz <see cref="IUserRepository"/>.
/// </summary>
internal class UserRepository : IUserRepository
{
    /// <summary>
    /// Referencia al cliente de acceso a la API REST.
    /// </summary>
    private readonly RestClient client;
    /// <summary>
    /// Referencia al mapeador entre <see cref="UserDTO"/> y <see cref="UserEntity"/>
    /// </summary>
    private readonly IMapper mapper;

    /// <summary>
    /// Construye una nueva instancia del repositorio de usuarios.
    /// </summary>
    /// <param name="client">Cliente de acceso a la API REST.</param>
    /// <param name="mapper">Mapeador de clases.</param>
    public UserRepository(RestClient client, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }

    /// <summary>
    /// Obtiene la lista de usuarios registrados en la REST API.
    /// </summary>
    /// <returns><see cref="IEnumerable{UserEntity}"/> con la lista de usuarios registrados en la API REST.</returns>
    public IEnumerable<UserEntity> getAll()
    {
        /// Prepara una petición GET a la API REST https://gorest.co.in/public/v1/users.
        RestRequest request = new RestRequest("https://gorest.co.in/public/v1/users", Method.Get);
        
        /// Ejecuta de manera asíncrona la petición de los usuarios registrados.
        var response = client.ExecuteAsync<ApiResponse>(request);
        /// Espera a que termine de ejecutar el método asíncrono.
        response.Wait();

        /// Mapea la lista de usuarios desde <see cref="UserDTO"/> a <see cref="UserEntity"/>.
        return mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserEntity>>(response.Result.Data.data);
    }
}
