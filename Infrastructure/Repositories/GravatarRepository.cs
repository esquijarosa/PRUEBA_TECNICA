using RestSharp;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio de imágenes de usuario para la interfaz <see cref="IGravatarRepository"/>.
    /// </summary>
    public class GravatarRepository : IGravatarRepository
    {
        /// <summary>
        /// Referencia al cliente de acceso a la API REST.
        /// </summary>
        private readonly RestClient client;

        /// <summary>
        /// Construye una nueva instancia del repositorio de imágenes de usuario.
        /// </summary>
        /// <param name="client">Cliente de acceso a la API REST.</param>
        public GravatarRepository(RestClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Obtiene la imagen del usuario representado por <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Identificador del usuarios del que se requiere su imagen.</param>
        /// <returns><see cref="byte[]"/> con los datos de la imagen del usuario.</returns>
        public byte[] getGravatar(string id)
        {
            /// Prepara la dirección del punto de acceso a la API de imágnes de usuarios
            /// con el identificador del usuario recibido en el parámetro 'id'.
            string url = $"https://www.gravatar.com/avatar/{id}?s=32&d=identicon&r=PG";
            /// Prepara una petición GET a la API REST representada por 'url'.
            RestRequest request = new RestRequest(url, Method.Get);

            /// Ejecuta de manera asíncrona la petición de la iamgen del usuario.
            var response = client.DownloadDataAsync(request);
            /// Espera a que termine de ejecutar el método asíncrono.
            response.Wait();

            /// Regresa el contenido de la imagen como un <see cref="byte[]"/>.
            return response.Result;

        }
    }
}
