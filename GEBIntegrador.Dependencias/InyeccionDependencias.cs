using GEBIntegrador.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using GEBIntegrador.Persistencia.Repositorios;
using GEBIntegrador.Infraestructura;
using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Core.Servicios;
using GEBIntegrador.Persistencia.Repositorios.Implementacion;
using GEBIntegrador.Core.Servicios.Implementacion;

namespace GEBIntegrador.Dependencias
{
    public static class InyeccionDependencias
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>{
                options.UseSqlServer(configuration.GetConnectionString("ConexionBD"));
            });
            
            services.AddScoped(typeof(IGenericRepositorio<>), typeof(GenericRepositorio<>));
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ISesionService, SesionService>();
            services.AddScoped<IPerfilService, PerfilService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<ICategoriaParqueaderoService, CategoriaParqueaderoService>();
            services.AddScoped<INivelService, NivelService>();
            services.AddScoped<IRecursoService, RecursoService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IParametrosService ,ParametrosService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));

        }
    }
}
