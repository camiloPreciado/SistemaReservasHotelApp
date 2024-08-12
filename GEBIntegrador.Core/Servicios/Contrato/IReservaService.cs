using GEBIntegrador.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface IReservaService
    {
        Task<reserva> ReservaParqueadero(reserva modelo);

        Task<reserva> ReservaRecurso(reserva modelo);

        Task<reserva> EditarReservaRecurso(reserva modelo);

        Task<bool> CambiarEstado(int n_id_usuario, int n_id);
        //Task<List<reserva>> ListarReservas(string tipoRecurso, string nivel);
        Task<List<reserva>> ListarReservas(string id);
    }
}
