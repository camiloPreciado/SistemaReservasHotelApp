import Enviroments from "../Shared/Enviroments.js";

let env = new Enviroments();
var baseUrl = `${window.location.origin}${env.FolderContenedor}`;

export default class ajaxService {
    post(url, data) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                //url: url,
                url: `${baseUrl}${url}`,
                content: "application/json; charset=utf-8",
                dataType: "json",
                data: data,
                success: function (d) {
                    resolve(d);
                },
                error: function (xhr, textStatus, errorThrown) {
                    reject({ success:false,  message: "Error" + xhr.responseText });
                }
            });
        });
    }
    postWithFile(url, data) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                //url: url,
                url: `${baseUrl}${url}`,
                processData: false,
                contentType: false,
                data: data,
                success: function (d) {
                    resolve(d);
                },
                error: function (xhr, textStatus, errorThrown) {
                    reject({ success:false,  message: "Error" + xhr.responseText });
                }
            });
        });
    }
    get(url, data) {
        let q = url;
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "GET",
                //url: url,
                url: `${baseUrl}${url}`,
                content: "application/json; charset=utf-8",
                dataType: "json",
                data: data,
                success: function (d) {
                    resolve(d);
                },
                error: function (xhr, textStatus, errorThrown) {
                    reject({ success:false,  message: "Error" + xhr.responseText });
                }
            });
        });
    }

    dowloadFile({ url, data, nameDowload }) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "GET",
                url: `${baseUrl}${url}`,
                data: data,
                content: "application/json; charset=utf-8",
                xhrFields: {
                    responseType: 'blob'
                },
                success: function (data) {
                    //if (xhr.getResponseHeader('Content-Type').includes('application/json')) {
                    //    // La respuesta es un objeto JSON (indicando un error)
                    //    const reader = new FileReader();
                    //    reader.onload = function () {
                    //        const errorResponse = JSON.parse(reader.result);
                    //        reject({ success: false, message: errorResponse.message });
                    //    };
                    //    reader.readAsText(data);
                    //} else {
                    //    // La respuesta es un Blob (archivo)
                    //    var link = document.createElement('a');
                    //    link.href = window.URL.createObjectURL(new Blob([data]));
                    //    link.download = nameDowload;
                    //    document.body.appendChild(link);
                    //    link.click();
                    //    document.body.removeChild(link);
                    //    resolve({ success: true });
                    //}

                    if (!!data) {
                        // La respuesta es un Blob (archivo)
                        var link = document.createElement('a');
                        link.href = window.URL.createObjectURL(new Blob([data]));
                        link.download = nameDowload;
                        document.body.appendChild(link);
                        link.click();
                        document.body.removeChild(link);
                        resolve({ success: true });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    resolve({ success: false, message: "Error al descarga el archivo. Comuniquese con el Administrador del sistema."});
                }
            });
        });
    }

    //dowloadFile({ url, data, nameDowload }) {
    //    let q = url;
    //    return new Promise((resolve, reject) => {
    //        $.ajax({
    //            type: "GET",
    //            url: `${baseUrl}${url}`,
    //            data: data,
    //            xhrFields: {
    //                responseType: 'blob'  // Indica que la respuesta será un objeto Blob
    //            },
    //            success: function (data, textStatus, xhr) {
    //                if (xhr.getResponseHeader('Content-Type') === 'application/json') {
    //                    // La respuesta es un objeto JSON (indicando un error)
    //                    const errorResponse = JSON.parse(String.fromCharCode.apply(null, new Uint8Array(data)));
    //                    reject({ success: false, message: errorResponse.message });
    //                } else {
    //                    // La respuesta es un Blob (archivo)
    //                    var link = document.createElement('a');
    //                    link.href = window.URL.createObjectURL(new Blob([data]));
    //                    link.download = nameDowload;
    //                    document.body.appendChild(link);
    //                    link.click();
    //                    document.body.removeChild(link);
    //                    resolve({ success: true });
    //                }
    //                //    // Crear un enlace temporal y simular un clic para iniciar la descarga
    //                //    var link = document.createElement('a');
    //                //    link.href = window.URL.createObjectURL(new Blob([data]));
    //                //    link.download = nameDowload;
    //                //    document.body.appendChild(link);
    //                //    link.click();
    //                //    document.body.removeChild(link);
    //            },
    //            error: function (xhr, textStatus, errorThrown) {
    //                reject({ success: false, message: "Error" + xhr.responseText });
    //            }
    //        });
    //    });
    //}

}
