using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Core.Services;

public static class ServicesExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection collection)
    {
        collection.AddScoped<ICodeService, CodeService>();
        return collection;
    }
}
