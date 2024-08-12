import ajaxService from "../Shared/AjaxService.js";
import Mensajes from "../Shared/Mensajes.js";
import FormValidator from "../Shared/FormValidator.js";


let msg = new Mensajes();
let ajax = new ajaxService();
let FormV = new FormValidator();


$(document).ready(function () {   
    var modelo = $("#form").data("model");

    $('#form').on('submit', function (e) {
        e.preventDefault();

        if ($('#form').valid()) {
            var formData = $(this).serializeArray();

            var formData = FormV.getFormValues(formData);


            var Data = { ...modelo, ...formData };
            ajax.post('/Niveles/EditarNivel', Data).then((d) => {
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
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}

