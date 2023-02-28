using System.Collections.Generic;

namespace Infrastructure.DTO
{
    /// <summary>
    /// Representa la respuesta que se obtiene de la API de usuarios.
    /// </summary>
    internal class ApiResponse
    {
        /// <summary>
        /// Lista de usuarios representados por <see cref="UserDTO"/>.
        /// </summary>
        public List<UserDTO> data { get; set; }
    }
}