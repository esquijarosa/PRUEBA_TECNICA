using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using RestSharp;

internal class UserRepository : IUserRepository
{
    private readonly RestClient client;
    private readonly IMapper mapper;

    public UserRepository(RestClient client, IMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }

    public IEnumerable<UserEntity> getAll()
    {
        RestRequest request = new RestRequest("https://gorest.co.in/public/v1/users", Method.Get);
        
        var response = client.ExecuteAsync<ApiResponse>(request);
        response.Wait();

        return mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserEntity>>(response.Result.Data.data);
    }
}
