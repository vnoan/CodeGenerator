using Microsoft.Extensions.DependencyInjection;

namespace TreasuryChallenge.Services
{
    internal static class ServicesExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection collection)
        {
            collection.AddScoped<ICodeService, CodeService>();
            return collection;
        }
    }
}
