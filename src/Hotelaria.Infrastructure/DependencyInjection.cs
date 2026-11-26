using Hotelaria.Application.Ports.Saida;
using Hotelaria.Infrastructure.Persistence;
using Hotelaria.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelaria.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HotelariaDbContext>(options => options.UseSqlite(connectionString));

            services.AddScoped<IHospedeRepository, HospedeRepository>();
            services.AddScoped<IQuartoRepository, QuartoRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();

            return services;
        }
    }
}
