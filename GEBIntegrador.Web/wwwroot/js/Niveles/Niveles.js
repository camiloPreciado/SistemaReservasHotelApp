import configDataTable from "../Shared/Datatables.js";
import ajaxService from "../Shared/AjaxService.js";
import Mensajes from "../Shared/Mensajes.js";

let msg = new Mensajes();
let ajax = new ajaxService();
$(document).ready(function () {
    let dt = new configDataTable();
    let table = new DataTable("#TablaNiveles", dt.dataTableConBotones());
});

function Registrar() {
    $("#parcial").html("");
    $("#parcial").load("/Niveles/_CrearPartial");
}

function Editar(id) {
    $("#parcial").html("");
    $("#parcial").load(`/Niveles/_EditarPartial/${id}`);
}

function Detalles(id) {
    $("#parcial").html("");
    $("#parcial").load(`/Niveles/_DetallePartial/${id}`);
}

function CambiarEstado(n_id, n_estado) {

    //let titlulo = 'Habilitar Recurso';
    let texto = "";
    let titlulo = "¿Está seguro que desea Habilitar el nivel?";
    if (n_estado == 1) {
        //texto = 'Deshabilitar Recurso';
        texto = '';
        titlulo = "¿Está seguro que desea Deshabilitar el nivel?";
    }

    msg.msgConfirmacionSiNo(titlulo, texto).then((result) => {
        if (result.isConfirmed)
            enviarCambio(n_id);
    });
}
function enviarCambio(n_id) {
    var data = new Object();
    data.n_id = n_id;

    ajax.post("/Niveles/CambiarEstado", data).then((d) => {
        procesarRespuesta(d);
    });
}

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

window.Registrar = Registrar;
window.Editar = Editar;
window.Detalles = Detalles;
window.CambiarEstado = CambiarEstado;