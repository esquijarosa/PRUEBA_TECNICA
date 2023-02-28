using Domain.Models;
using System.Collections.Generic;

namespace Domain.Services
{
    /// <summary>
    /// Intefaz que representa el servicio de almacenamiento en disco de la imágenes de los usuarios.
    /// </summary>
    public interface IGravatarToDiskService
    {
        /// <summary>
        /// Almacena en disco las imágenes de los usuarios contenidos en <paramref name="users"/>.
        /// </summary>
        /// <param name="users">Lista de usuarios de los que se quiere almacenar su imagen en disco.</param>
        /// <returns><see cref="true"/> si se han podido almacenar las imágenes correctamente. De lo contrario
        /// <see cref="false"/>.</returns>
        bool saveGravatarFromUsers(IEnumerable<UserEntity> users);
    }
}