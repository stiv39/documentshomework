using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IDocumentRepositoryService, DocumentRepositoryService>();
            services.AddScoped<IFormatterService, FormatterService>();
            services.AddSingleton<IDocumentInMemoryService, DocumentInMemoryService>();

            return services;
        }
    }
}
