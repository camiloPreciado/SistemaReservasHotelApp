using Microsoft.AspNetCore.Mvc;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using GEBIntegrador.Core.Servicios.Contrato;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;


namespace GEBIntegrador.Web.Controllers
{
    [Authorize]
    public class ParametrosController : Controller
    {
        private readonly IParametrosService _parametroServicio;
        private readonly IMapper _mapper;

        public ParametrosController(IParametrosService parametroServicio,
                                  IMapper mapper)
        {
            _parametroServicio = parametroServicio;
            _mapper = mapper;
        }


        public async Task<IActionResult> Lista()
        {
            return View(_mapper.Map<List<ParametroDto>>(await _parametroServicio.ListarParametros()));
        }


        public async Task<IActionResult> _CrearPartial()
        {
            return PartialView();
        }


        public async Task<IActionResult> _EditarPartial(int id)
        {
            ParametroDto ParametroEncontrado = _mapper.Map<ParametroDto>(await _parametroServicio.ObtenerParametro(id));
            return PartialView(ParametroEncontrado);
        }


        [HttpGet]
        public async Task<JsonResult> ObtenerParametros(int id)
        {
            try
            {
                parametro parametros = await _parametroServicio.ObtenerParametro(id);
                return Json(new { success = true, data = parametros});
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CrearNuevoParametro(ParametroDto parametro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _parametroServicio.CrearParametro(_mapper.Map<parametro>(parametro));
                    return Json(new RespuestaDto { success = true, message = "Parametro creado exitosamente" });
                }
                else
                {
                    // Hay errores de validación, devolver los errores
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
                    return Json(new RespuestaDto { success = false, messages = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }


        [HttpPost]
        public async Task<IActionResult> EditarParametro(ParametroDto parametro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _parametroServicio.EditarParametro(_mapper.Map<parametro>(parametro));
                    return Json(new RespuestaDto { success = true, message = "Parametro editado exitosamente" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
                    return Json(new RespuestaDto { success = false, messages = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }
    }
}
