using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

internal class GravatarToDiskService : IGravatarToDiskService
{
    private readonly IGravatarRepository gravatarRepository;

    public GravatarToDiskService(IGravatarRepository gravatarRepository)
    {
        this.gravatarRepository = gravatarRepository;  
    }

    public bool saveGravatarFromUsers(IEnumerable<UserEntity> users)
    {
        foreach(UserEntity user in users)
        {
            string hashedEmail = generateHash(user.emailAddress);

            byte[] binaryGravatar = gravatarRepository.getGravatar(hashedEmail);

            Stream imageFile = new FileStream($"./{user.id}.png", FileMode.Create);
            imageFile.Write(binaryGravatar);
            imageFile.Close(); 
        }

        return true;
    }

    private string generateHash(string email)
    {
        byte[] data = Encoding.ASCII.GetBytes(email);

        data = MD5.Create().ComputeHash(data);

        return Convert.ToHexString(data).ToLower();
    }
}
