import configDataTable from "../Shared/Datatables.js";
import ajaxService from "../Shared/AjaxService.js";
import Mensajes from "../Shared/Mensajes.js";

let msg = new Mensajes();
let ajax = new ajaxService();
$(document).ready(function () {
    let dt = new configDataTable();
    let table = new DataTable("#TablaRecursos", dt.dataTableSinBotones());
    $('#Modal').on('hidden.bs.modal', function () {
        location.reload();
    });
});

function Registrar() {
    $("#parcial").html("");
    $("#parcial").load("/Recursos/_CrearPartial");
}

function Editar(id) {
    $("#parcial").html("");
    $("#parcial").load(`/Recursos/_EditarPartial/${id}`);
}

function Detalles(id) {
    $("#parcial").html("");
    $("#parcial").load(`/Recursos/_DetallePartial/${id}`);
}

function CambiarEstado(n_id, n_estado) {

    let titlulo = '¿Habilitar Recurso?';
    let texto = "¿Está seguro que desea HABILITAR el recurso?";
    if (n_estado == 1) {
        titlulo = '¿Deshabilitar Recurso?';
        texto = "¿Está seguro que desea DESHABILITAR el recurso?";
    }

    msg.msgConfirmacion(titlulo, texto).then((result) => {
        if (result.isConfirmed)
            enviarCambio(n_id);
    });
}
function enviarCambio(n_id) {
    var data = new Object();
    data.n_id = n_id;

    ajax.post("/Recursos/CambiarEstado", data).then((d) => {
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