using Microsoft.EntityFrameworkCore;
using ProjectManagement.EntityFramework;

namespace ProjectManagementAPI.Configurations.Persistance
{
    public static class PersistanceConfiguration
    {
        public static void AddCustomDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PManagementDB");

            services.AddDbContext<PManagementDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    var assembly = typeof(PManagementDbContext).Assembly;
                    var assemblyName = assembly.GetName();

                    sqlServerOptions.MigrationsAssembly(assemblyName.Name);
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 2,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
        }
    }
}
