import ajaxService from "../AjaxService.js";

export default class AreaService {
    constructor() {
        this.ajax = new ajaxService();
        this.url = "/Area/";
    }
    getAreas(data) {
        return new Promise((resolve) => {
            this.ajax.get(`${this.url}getAreas`, data).then((response) => {
                resolve(response);
            });
        });
    }
}