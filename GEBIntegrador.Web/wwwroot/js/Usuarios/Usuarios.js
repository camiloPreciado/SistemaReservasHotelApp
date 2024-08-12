import configDataTable from "../Shared/Datatables.js";
import Enviroments from "../Shared/Enviroments.js";

let env = new Enviroments();
var baseUrl = `${window.location.origin}${env.FolderContenedor}`;

$(document).ready(function () {
    let dt = new configDataTable();
    let table = new DataTable("#UserTable", dt.dataTableConBotones("Usuarios")); 
});

function Registrar() {

    $("#parcial").html("");
    $("#parcial").load(`${baseUrl}/Usuarios/_RegistrarPartial`, function () {
        $("#Modal").modal("show");
    });
}
function Editar(id) {
    $("#parcial").html("");
    $("#parcial").load(`${baseUrl}/Usuarios/_EditPartial/${id}`, function () {
        $("#Modal").modal("show");
    });
}
function Detalles(id) {
    $("#parcial").html("");
    $("#parcial").load(`${baseUrl}/Usuarios/_DetailsParcial/${id}`, function () {
        $("#Modal").modal("show");
    });
}
function Eliminar(id) {
    $("#parcial").html("");
    $("#parcial").load(`${baseUrl}/Usuarios/_DeleteParcial/${id}`, function () {
        $("#Modal").modal("show");
    });
    //$("#parcial").load("/Usuarios/_DeleteParcial/" + id);
}

window.Detalles = Detalles;
window.Editar = Editar;
window.Registrar = Registrar;
window.Eliminar = Eliminar;