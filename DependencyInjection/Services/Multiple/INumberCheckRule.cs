using System.Threading.Tasks;

namespace DependencyInjection.Services.Multiple
{
    public interface INumberCheckRule
    {
        Task<bool> CompliesWithRuleAsync(int number);

        string ErrorMessage { get; }
    }
}