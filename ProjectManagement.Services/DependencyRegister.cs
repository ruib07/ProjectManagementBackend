using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Services.Services;
using Scrutor;

namespace ProjectManagement.Services
{
    public static class DependencyRegister
    {
        public static void AddCustomServiceDependencyRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .Scan(scan => scan.FromAssemblies(typeof(RegisterUserService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(UserService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(CommentsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ProjectMembersService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ProjectsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(StagesService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(TasksService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        }
    }
}