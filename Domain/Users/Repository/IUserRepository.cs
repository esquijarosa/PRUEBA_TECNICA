using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository 
{
    IEnumerable<UserEntity> getAll();
}