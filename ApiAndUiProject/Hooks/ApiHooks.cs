using dotenv.net;
using ApiAndUiProject.API.Clients;
using ApiAndUiProject.API.Context;
using Reqnroll;

namespace ApiAndUiProject.Hooks
{
    [Binding]
    public class ApiHooks(ApiContext context, UsersApiClient usersApiClient)
    {
        private readonly ApiContext _context = context;
        private readonly UsersApiClient _usersApiClient = usersApiClient;

        [AfterScenario]
        public void CleanupCreatedUsers()
        {
            var createdUserIds = _context.Get<List<int>>("CreatedUserIds") ?? new List<int>();

            foreach (var id in createdUserIds.Distinct())
            {
                try
                {
                    var resp = _usersApiClient.DeleteUser(id);
                    Console.WriteLine($"Deleted user with id: {id}, status: {resp.StatusCode}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete user with id: {id}. Error: {ex.Message}");
                }
            }

            createdUserIds.Clear();
            _context.Set("CreatedUserIds", createdUserIds);
        }
    }
}