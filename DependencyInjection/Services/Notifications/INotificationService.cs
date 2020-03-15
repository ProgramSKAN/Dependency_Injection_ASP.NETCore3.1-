using System.Threading.Tasks;

namespace DependencyInjection.Services.Notifications
{
    public interface INotificationService
    {
        Task SendAsync(string message, string userId);
    }
}