using AutoMapper;
using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dominio.Mensajes;
using GEBIntegrador.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GEBIntegrador.Web.Controllers
{
    [Authorize]
    public class NivelesController : Controller
    {
        private readonly INivelService _nivelServicio;

        private readonly IMapper _mapper;

        public NivelesController(INivelService nivelServicio,
                                IMapper mapper)
        {
            _nivelServicio = nivelServicio;
            _mapper = mapper;
        }

   

        public async Task<IActionResult> Index()
        {
            
            return View(_mapper.Map<List<NivelesDto>>(await _nivelServicio.CargarNiveles()));
        }

        public async Task<IActionResult> _CrearPartial()
        {
            return PartialView();
        }

        public async Task<IActionResult> _EditarPartial(int id)
        {
            return PartialView(_mapper.Map<NivelesDto>(await _nivelServicio.CargarNivel(id)));
        }

        public async Task<IActionResult> _DetallePartial(int id)
        {
            return PartialView(_mapper.Map<NivelesDto>(await _nivelServicio.CargarNivel(id)));
        }


        [HttpPost]
        public async Task<IActionResult> CrearNivel(NivelesDto nivel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _nivelServicio.CrearNivel(_mapper.Map<niveles>(nivel));
                    return Json(new RespuestaDto { success = true, message = "Nivel creado exitosamente" });
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
        public async Task<IActionResult> EditarNivel(NivelesDto nivel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _nivelServicio.EditarNivel(_mapper.Map<niveles>(nivel));
                    return Json(new RespuestaDto { success = true, message = "Nivel editado exitosamente" });
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
        public async Task<IActionResult> CambiarEstado(int n_id)
        {
            try
            {
                if (n_id != null)
                {
                    if (await _nivelServicio.CambiarEstado(n_id))
                        return Json(new RespuestaDto { success = true, message = MensajesSistema.MsgEstadoCambiado });
                    else
                        return Json(new RespuestaDto { success = false, message = MensajesSistema.MsgNosePudoCambiarElEstado });
                }
                else
                {
                    return Json(new RespuestaDto { success = false, message = MensajesSistema.MsgNosePudoCambiarElEstado });
                }
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }

    }

}
