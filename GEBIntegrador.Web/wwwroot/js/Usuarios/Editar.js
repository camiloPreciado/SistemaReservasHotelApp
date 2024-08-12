import UsuarioService from "../Shared/Servicios/UsuarioService.js";
import Mensajes from "../Shared/Mensajes.js";
import FormValidator from "../Shared/FormValidator.js";

let msg = new Mensajes();
let usuarioServicio = new UsuarioService();
let FormV = new FormValidator();

$(document).ready(function () {

    var modelo = $('#form_edit').data('model');

    $('#form_edit').on('submit', function (e) {
        e.preventDefault();

        if ($('#form_edit').valid()) {
            var formData = $(this).serializeArray();

            var formData = FormV.getFormValues(formData);

            var Data = { ...modelo, ...formData };

            usuarioServicio.guardar(Data).then((d) => {
                procesarRespuesta(d);
            });

            //p.post('/Usuarios/_EditPartial', formData).then((d) => {
            //     procesarRespuesta(d);
            //}); 
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
