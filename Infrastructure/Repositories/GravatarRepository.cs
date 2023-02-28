using RestSharp;
using Domain.Repositories;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio de imágenes de usuario para la interfaz <see cref="IGravatarRepository"/>.
    /// </summary>
    public class GravatarRepository : IGravatarRepository
    {
        private readonly RestClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IGravatarRepository> _logger;

        /// <summary>
        /// Construye una nueva instancia del repositorio de imágenes de usuario.
        /// </summary>
        /// <param name="client">Cliente de acceso a la API REST.</param>
        /// <param name="configuration">Configuración de la aplicación.</param>
        public GravatarRepository(RestClient client, IConfiguration configuration, ILogger<IGravatarRepository> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la imagen del usuario representado por <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Identificador del usuarios del que se requiere su imagen.</param>
        /// <returns><see cref="byte[]"/> con los datos de la imagen del usuario.</returns>
        public byte[] GetGravatar(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("El identificador del usuario no puede estar vacío.");
                }

                string url = string.Format(_configuration["apiEndpoints:gravatarEndpoint"], id);
                RestRequest request = new RestRequest(url, Method.Get);

                var response = _client.DownloadDataAsync(request);
                response.Wait();

                return response.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
