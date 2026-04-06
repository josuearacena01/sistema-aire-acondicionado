using AireApi.Application.Services;
using AireApi.Domain.Interfaces;
using AireApi.Infrastructure.Data;
using AireApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AireApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Conexión
            services.AddSingleton(new DbConnectionFactory(connectionString));

            // Repositorios
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IVehiculoRepository, VehiculoRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IServicioRepository, ServicioRepository>();
            services.AddScoped<IDetalleServicioRepository, DetalleServicioRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICatalogoRepository, CatalogoRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            // Servicios
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVehiculoService, VehiculoService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<IDetalleServicioService, DetalleServicioService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IFacturaService, FacturaService>();
            services.AddScoped<IPagoService, PagoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICatalogoService, CatalogoService>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}
