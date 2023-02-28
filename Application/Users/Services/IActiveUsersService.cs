using System.Collections.Generic;

public interface IActiveUsersService
{
    IEnumerable<UserEntity> getActiveUsers();
}
