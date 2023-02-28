using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    /// <summary>
    /// Implementación del servicio de almacenamiento en disco de las imágenes de usuario para la interfaz <see cref="IGravatarToDiskService"/>.
    /// </summary>
    public class GravatarToDiskService : IGravatarToDiskService
    {
        private readonly IGravatarRepository _gravatarRepository;
        private readonly ILogger<IGravatarRepository> _logger;

        /// <summary>
        /// Construye una nueva instancia del servicio de almacenamiento en disco de imágenes de usuario.
        /// </summary>
        /// <param name="gravatarRepository">Repositorio de imágenes de usuario.</param>
        /// <param name="logger">Logging infrastructure.</param>
        public GravatarToDiskService(IGravatarRepository gravatarRepository, ILogger<IGravatarRepository> logger)
        {
            _gravatarRepository = gravatarRepository;
            _logger = logger;
        }

        /// <summary>
        /// Almacena en disco las imágenes de los usuarios contenidos en <paramref name="users"/>.
        /// </summary>
        /// <param name="users">Lista de usuarios de los que se quiere almacenar su imagen en disco.</param>
        /// <returns><see cref="true"/> si se han podido almacenar las imágenes correctamente. De lo contrario
        /// <see cref="false"/>.</returns>
        public bool SaveGravatarFromUsers(IEnumerable<UserEntity> users)
        {
            try
            {
                foreach (UserEntity user in users)
                {
                    string hashedEmail = GenerateHash(user.emailAddress);

                    byte[] binaryGravatar = _gravatarRepository.GetGravatar(hashedEmail);

                    using Stream imageFile = new FileStream($"./{user.id}.png", FileMode.Create);
                    imageFile.Write(binaryGravatar);
                    imageFile.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Genera una llave HASH basada en el correo electrónico de un usuario.
        /// </summary>
        /// <param name="email"><see cref="string"/> reprentando una dirección de correo electrónico.</param>
        /// <returns>HASH MD5 de la dirección de correo electrónico representada en <paramref name="email"/>.</returns>
        private static string GenerateHash(string email)
        {
            byte[] data = Encoding.ASCII.GetBytes(email);

            data = MD5.Create().ComputeHash(data);

            return Convert.ToHexString(data).ToLower();
        }
    }
}