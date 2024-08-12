export default class configDataTable {
    dataTableSinBotones() {
        let table = {
            "oLanguage": {
                "sEmptyTable": "No se encontraron registros",
                "sInfoEmpty": "Mostrando 0 to 0 of 0 registros",
                "sInfo": "Registros _START_ a  _END_ de _TOTAL_",
                "sSearch": "Filtrar en página:",
                "sInfoFiltered": " (Filtrado de _MAX_ registros)",
                "sZeroRecords": "No se encontraron coincidencias con el filtro",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primera Pagina",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                }
            },
            "info": true
        };
        return table;
    }
    dataTableConBotones(tituloExpor) {
        let table = {
            "oLanguage": {
                "sEmptyTable": "No se encontraron registros",
                "sInfoEmpty": "Mostrando 0 to 0 of 0 registros",
                "sInfo": "Registros _START_ a  _END_ de _TOTAL_",
                "sSearch": "Filtrar en página:",
                "sInfoFiltered": " (Filtrado de _MAX_ registros)",
                "sZeroRecords": "No se encontraron coincidencias con el filtro",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primera Pagina",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                }
            },
            "info": true,
            dom: "Bfrtip",
            buttons: [
                {
                    extend: "excelHtml5",
                    text: "Exportar Excel",
                    filename: tituloExpor,
                    title: tituloExpor,
                },
                {
                    extend: "print",
                    text: "Imprimir",
                    title: tituloExpor,
                },
                "pageLength"
            ],

        };
        return table;

    }
}