using PlaywrightProject.API.Context;
using Reqnroll;

[Binding]
public class ApiCleanupHooks
{
    private readonly UsersApiClient _api;
    private readonly UsersApiContext _context;

    public ApiCleanupHooks(UsersApiContext context)
    {
        _api = new UsersApiClient("http://localhost:5043");
        _context = context;
    }

    [AfterScenario]
    public void CleanupCreatedUsers()
    {
        foreach (var id in _context.CreatedUserIds.Distinct())
        {
            try
            {
                var resp = _api.DeleteUser(id);
                Console.WriteLine($"[CLEANUP] Deleted user with id: {id}, status: {resp.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CLEANUP] Failed to delete user with id: {id}. Error: {ex.Message}");
            }
        }
        _context.CreatedUserIds.Clear();
    }
}