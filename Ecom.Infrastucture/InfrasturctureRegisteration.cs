using Ecom.Core.Interfaces;
using Ecom.Infrastucture.Data;
using Ecom.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.Infrastucture
{
    public static class InfrasturctureRegisteration
    {
        public static IServiceCollection InfrasturctureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // apply unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // apply DbContext
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
            });
            return services;
        }
    }
}