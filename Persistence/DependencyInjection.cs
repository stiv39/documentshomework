using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(assembly.FullName));
            });

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.Decorate<IDocumentRepository, CachedDocumentRepository>();

            services.AddMemoryCache();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
