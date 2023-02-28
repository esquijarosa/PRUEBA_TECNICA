using System.Collections.Generic;

/// <summary>
/// Interfaz que representa el servicio de obtención de usuarios activos.
/// </summary>
public interface IActiveUsersService
{
    /// <summary>
    /// Obtiene la lista de usuarios activos.
    /// </summary>
    /// <returns><see cref="IEnumerable{UserEntity}"/> con la lista de usuarios activos.</returns>
    IEnumerable<UserEntity> getActiveUsers();
}
