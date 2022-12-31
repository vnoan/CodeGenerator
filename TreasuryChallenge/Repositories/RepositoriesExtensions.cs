using Microsoft.Extensions.DependencyInjection;

namespace TreasuryChallenge.Repositories
{
    internal static class RepositoriesExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<ICodeRepository, CodeRepository>();
            return collection;
        }
    }

}
