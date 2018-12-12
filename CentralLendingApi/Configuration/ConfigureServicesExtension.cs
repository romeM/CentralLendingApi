using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace CentralLendingApi.Configuration
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services, string assemblyName)
        {
            var assemblyPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), assemblyName + ".dll");
            var assembly = Assembly.Load(AssemblyLoadContext.GetAssemblyName(assemblyPath));

            var classTypes = assembly.ExportedTypes.Select(t => IntrospectionExtensions.GetTypeInfo(t)).Where(t => t.IsClass && !t.IsAbstract && t.FullName.EndsWith("Service"));

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces)
                    services.AddTransient(handlerType.AsType(), type.AsType());
            }

            return services;
        }
    }
}
