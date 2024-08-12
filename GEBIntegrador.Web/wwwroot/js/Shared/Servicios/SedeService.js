import ajaxService from "../AjaxService.js";

export default class SedeService {
    constructor() {
        this.ajax = new ajaxService();
        this.url = "/Sede/";
    }
    getSedes(data) {
        return new Promise((resolve) => {
            this.ajax.get(`${this.url}getSedes`, data).then((response) => {
                resolve(response);
            });
        });
    }

    getSedeId(data) {
        return new Promise((resolve) => {
            this.ajax.get(`${this.url}getSedeId`, data).then((response) => {
                resolve(response);
            });
        });
    }
}