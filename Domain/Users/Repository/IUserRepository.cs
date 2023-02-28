using System.Collections.Generic;

/// <summary>
/// Interfaz que representa un repositorio de usuarios.
/// </summary>
public interface IUserRepository 
{
    /// <summary>
    /// Obtiene la lista de todos los usuarios registrados.
    /// </summary>
    /// <returns><see cref="IEnumerable{UserEntity}"/> con la lista de los usuarios registrados.</returns>
    IEnumerable<UserEntity> getAll();
}