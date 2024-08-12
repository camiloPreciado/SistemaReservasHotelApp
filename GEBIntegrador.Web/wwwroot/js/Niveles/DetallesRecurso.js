
function tipoRecursoElegido(n_id) {

    // Obtener el contenedor del bloque Categoria Parqueadero
    var capacidadRecurso = document.getElementById('capacidadRecurso');
    var categoriaRecurso = document.getElementById('categoriaRecurso');


    if (n_id == 1) {
        // Mostrar el bloque Categoria Parqueadero
        categoriaRecurso.style.display = 'block';        
        capacidadRecurso.style.display = 'none';

    } else if (n_id == 3) {
        // Ocultar el bloque Categoria Parqueadero
        categoriaRecurso.style.display = 'none';        
        capacidadRecurso.style.display = 'block';
    } else {
        categoriaRecurso.style.display = 'none';
        capacidadRecurso.style.display = 'none';
    }
   
}



$(document).ready(function () {
    var modelo = $("#form").data("model");
    if (modelo) {
        document.getElementById('n_tipo_recurso').selectedIndex = modelo.n_tipo_recurso;
        tipoRecursoElegido(modelo.n_tipo_recurso);
        
    }

});

window.tipoRecursoElegido = tipoRecursoElegido;