using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface ISesionService
    {
        Task<List<Claim>> IniciarSesion(LoginDto modelo);
        SesionDto ObtenerSesion(HttpContext httpContext);

    }
}
