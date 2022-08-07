using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using System.Reflection;
using System.Collections;
using KanBanApi.Implementation;

namespace KanBanSpecFlow.Support;

public static class SpecFlowDependencyInjection
{

    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        BuildService(services).GetAwaiter().GetResult();

        return services;
    }

    public static async Task BuildService(IServiceCollection services)
    {
        var executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        services.AddLogging();

        var config = BuildConfig(services).GetAwaiter().GetResult();

        services.AddSingleton(config);

        services.AddDrivers();
    }

    public static async Task<IConfiguration> BuildConfig(IServiceCollection services)
    {
        foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables())
        {
            if (environmentVariable.Key.ToString()!.Contains("APPLICATION") && environmentVariable.Value!.ToString()!.StartsWith('/'))
                Environment.SetEnvironmentVariable(environmentVariable.Key.ToString()!, await File.ReadAllTextAsync(environmentVariable.Value.ToString()!));
        }

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        return configuration;
    }

}
