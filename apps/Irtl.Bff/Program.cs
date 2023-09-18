using Irtl.Bff.Links;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Irtl.Bff;

internal static class Program
{
    public static void Main(string[] args)
    {
        var host = new HostBuilder() 
            .ConfigureFunctionsWorkerDefaults((context, builder) =>
            {
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton<IOpenApiHttpTriggerAuthorization>(_ =>
                {
                    var auth = new OpenApiHttpTriggerAuthorization(req =>
                    {
                        var result = default(OpenApiAuthorizationResult);
                        // ⬇️⬇️⬇️ Add your custom authorisation logic ⬇️⬇️⬇️
                        //
                        // CUSTOM AUTHORISATION LOGIC
                        //
                        // ⬆️⬆️⬆️ Add your custom authorisation logic ⬆️⬆️⬆️

                        return Task.FromResult(result);
                    });

                    return auth;

                });
                services.AddHttpClient();
                services.AddSingleton<ILinksStore, InMemoryLinksStore>();
            })
            .Build();

        host.Run();
    }
}