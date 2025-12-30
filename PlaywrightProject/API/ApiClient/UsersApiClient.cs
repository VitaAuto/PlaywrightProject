using RestSharp;
using PlaywrightProject.API.Models;

public class UsersApiClient
{
    private readonly RestClient _client;

    public UsersApiClient(string baseUrl)
    {
        _client = new RestClient(baseUrl);
    }

    public RestResponse CreateUser(User user)
    {
        var request = new RestRequest("/api/users", Method.Post);
        request.AddJsonBody(user);
        return _client.Execute(request);
    }

    public RestResponse GetUser(int id)
    {
        var request = new RestRequest($"/api/users/{id}", Method.Get);
        return _client.Execute(request);
    }

    public RestResponse GetAllUsers()
    {
        var request = new RestRequest("/api/users", Method.Get);
        return _client.Execute(request);
    }

    public RestResponse UpdateUser(int id, User user)
    {
        var request = new RestRequest($"/api/users/{id}", Method.Put);
        request.AddJsonBody(user);
        return _client.Execute(request);
    }

    public RestResponse PatchUser(int id, object patchDto)
    {
        var request = new RestRequest($"/api/users/{id}", Method.Patch);
        request.AddJsonBody(patchDto);
        return _client.Execute(request);
    }

    public RestResponse DeleteUser(int id)
    {
        var request = new RestRequest($"/api/users/{id}", Method.Delete);
        return _client.Execute(request);
    }
}