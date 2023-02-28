using System.Collections.Generic;

internal class ActiveUsersService : IActiveUsersService
{
    private readonly IUserRepository userRepository;

    public ActiveUsersService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public IEnumerable<UserEntity> getActiveUsers()
    {
        IEnumerable<UserEntity> users = userRepository.getAll();

        List<UserEntity> activeUsers = new List<UserEntity>();

        foreach (UserEntity user in users)
        {
            if (user.status == "active")
            {
                activeUsers.Add(user);
            }

        }

        return activeUsers;
    }
}
