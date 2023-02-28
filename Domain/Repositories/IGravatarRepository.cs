namespace Domain.Repositories
{
    /// <summary>
    /// Interfaz que representa un repositorio de imágenes de usuario.
    /// </summary>
    public interface IGravatarRepository
    {
        /// <summary>
        /// Obtiene la imagen del usuario representado por <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Identificador del usuarios del que se requiere su imagen.</param>
        /// <returns><see cref="byte[]"/> con los datos de la imagen del usuario.</returns>
        byte[] GetGravatar(string id);
    }
}