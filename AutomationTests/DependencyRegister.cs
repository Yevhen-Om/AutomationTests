using AutomationTests.APITests.APIService;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;

namespace AutomationTests
{
    [SetUpFixture]
    public static class DependencyRegister
    {
        private static ServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.RegisterCustomLogicServices()
                .RegisterAPIService();

            _serviceProvider = services.BuildServiceProvider();
        }

        private static IServiceCollection RegisterCustomLogicServices(this IServiceCollection services)
        {
            var config = new XmlLoggingConfiguration("NLog.config");
            LogManager.Configuration = config;

            services.AddSingleton<ILogger>(provider => LogManager.GetCurrentClassLogger());

            return services;
        }

        private static IServiceCollection RegisterAPIService(this IServiceCollection services)
        {
            services.AddSingleton<APIService>();

            return services;
        }

        [OneTimeTearDown]
        public static void Cleanup()
        {
            _serviceProvider?.Dispose();
        }

        public static T GetService<T>() => _serviceProvider.GetRequiredService<T>();
    }
}
