using System.Threading.Tasks;

namespace ApiControllerProject.Services
{
    public interface ISqsInitializerService
    {
        Task<string> EnsureQueueExistsAsync(string queueName);
    }
}