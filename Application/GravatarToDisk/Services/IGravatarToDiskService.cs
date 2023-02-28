using System.Collections.Generic;

public interface IGravatarToDiskService
{
    bool saveGravatarFromUsers(IEnumerable<UserEntity> users);
}