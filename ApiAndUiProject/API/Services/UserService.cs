using ApiAndUiProject.API.Clients;
using ApiAndUiProject.API.Models;
using Newtonsoft.Json;

namespace ApiAndUiProject.API.Services
{
    public class UserService
    {
        private readonly UsersApiClient _usersApiClient;

        public UserService(UsersApiClient usersApiClient)
        {
            _usersApiClient = usersApiClient;
        }

        public void EnsureUserEmailIsUnique(string email)
        {
            var response = _usersApiClient.GetAllUsers();
            var users = JsonConvert.DeserializeObject<List<User>>(response.Content) ?? new List<User>();
            var usersWithSameEmail = users.Where(u => u.Email == email).ToList();

            foreach (var user in usersWithSameEmail)
            {
                _usersApiClient.DeleteUser(user.Id);
            }
        }
    }
}