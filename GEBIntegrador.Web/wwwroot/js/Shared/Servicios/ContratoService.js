import ajaxService from "../AjaxService.js";

export default class ContratoService {
    constructor() {
        this.ajax = new ajaxService();
        this.url = "/Contrato/";
    }
    getContrato(data) {
        return new Promise((resolve) => {
            this.ajax.get(`${this.url}getContrato`, data).then((response) => {
                resolve(response);
            });
        });
    }
}