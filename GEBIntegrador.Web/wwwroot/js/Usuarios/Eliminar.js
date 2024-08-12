import UsuarioService from "../Shared/Servicios/UsuarioService.js";
import Mensajes from "../Shared/Mensajes.js";

let msg = new Mensajes();
let usuarioServicio = new UsuarioService();


$(document).ready(function () {
    $('#form').on('submit', function (e) {
        e.preventDefault();

        if ($('#form').valid()) {
            var formData = $(this).serialize();

            //p.post('/Usuarios/DeleteConfirmed', formData).then((d) => {
            usuarioServicio.eliminar(formData).then((d) => {
                 procesarRespuesta(d);
            }); 
        }
    });
});
function procesarRespuesta(d) {
    if (d.success) {
        msg.msgSuccess(d.message).then((result) => {
            window.location.reload();
        });
    } else {
        if(!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}
