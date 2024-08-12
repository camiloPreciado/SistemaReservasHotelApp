using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GebIntegrador.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GebIntegrador.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISesionService _sesionServicio;

        public HomeController(ILogger<HomeController> logger, ISesionService sesionServicio)
        {
            _logger = logger;
            _sesionServicio = sesionServicio;
        }

        public IActionResult Index()
        {
            ViewData["nombreUsuario"] = _sesionServicio.ObtenerSesion(HttpContext).v_nombre;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}