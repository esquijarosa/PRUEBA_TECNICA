using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Implementación del servicio de almacenamiento en disco de las imágenes de usuario para la interfaz <see cref="IGravatarToDiskService"/>.
/// </summary>
internal class GravatarToDiskService : IGravatarToDiskService
{
    /// <summary>
    /// Referencia al repositorio de imágenes de usuario.
    /// </summary>
    private readonly IGravatarRepository gravatarRepository;

    /// <summary>
    /// Construye una nueva instancia del servicio de almacenamiento en disco de imágenes de usuario.
    /// </summary>
    /// <param name="gravatarRepository">Repositorio de imágenes de usuario.</param>
    public GravatarToDiskService(IGravatarRepository gravatarRepository)
    {
        this.gravatarRepository = gravatarRepository;  
    }

    /// <summary>
    /// Almacena en disco las imágenes de los usuarios contenidos en <paramref name="users"/>.
    /// </summary>
    /// <param name="users">Lista de usuarios de los que se quiere almacenar su imagen en disco.</param>
    /// <returns><see cref="true"/> si se han podido almacenar las imágenes correctamente. De lo contrario
    /// <see cref="false"/>.</returns>
    public bool saveGravatarFromUsers(IEnumerable<UserEntity> users)
    {
        /// Para cada usuario en la lista de usuarios...
        foreach(UserEntity user in users)
        {
            /// Genera el HASH para el correo electrónico del usuario actual.
            string hashedEmail = generateHash(user.emailAddress);

            /// Obtiene la imagen del usuario.
            byte[] binaryGravatar = gravatarRepository.getGravatar(hashedEmail);

            /// Crea un archivo de imagen PNG cuyo nombre corresponde al identificador del usuario actual.
            Stream imageFile = new FileStream($"./{user.id}.png", FileMode.Create);
            /// Escribe los datos de la imagen al archivo.
            imageFile.Write(binaryGravatar);
            /// Cierra el flujo de datos para que se pueda crear el archivo en disco.
            imageFile.Close(); 
        }

        return true;
    }

    /// <summary>
    /// Genera una llave HASH basada en el correo electrónico de un usuario.
    /// </summary>
    /// <param name="email"><see cref="string"/> reprentando una dirección de correo electrónico.</param>
    /// <returns>HASH MD5 de la dirección de correo electrónico representada en <paramref name="email"/>.</returns>
    private string generateHash(string email)
    {
        /// Obtiene la representación en bytes de la dirección de correo electrónico
        /// utilizando codificación ASCII.
        byte[] data = Encoding.ASCII.GetBytes(email);

        /// Genera el HASH utilizando el algoritmo de digestión MD5
        data = MD5.Create().ComputeHash(data);

        /// Regresa una cadena de texto con la representación hexadecimal del HASH
        /// con todos los caracteres en minúsculas.
        return Convert.ToHexString(data).ToLower();
    }
}
