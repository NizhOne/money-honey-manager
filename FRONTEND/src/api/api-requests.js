import { callServer } from "../utils/request-util";

const apiUrl = "http://suslovvladimir-001-site1.htempurl.com/api/";

export default class ApiRequests {
    static getHeaders() {
        return {
            "content-type": "application/json"
        };
    }

    registration(name, email, password) {
        return new Promise(() => {
            callServer({
                url: apiUrl + "Auth/Register",
                method: "POST",
                headers: ApiRequests.getHeaders(),
                data: {
                    name,
                    email,
                    password
                }
            }).then(
                response => {
                    console.log(response);
                    if ((response.status === 200)) {
                        localStorage.setItem("token", `Bearer ${response.data}`);
                    }
                },
                error => {
                    console.log(error.response);
                });
        });
    }

    authorization(email, password) {
        return new Promise(() => {
            callServer({
                url: apiUrl + "Auth/Login",
                method: "POST",
                headers: ApiRequests.getHeaders(),
                data: {
                    email,
                    password
                }
            }).then(response => {
                if ((response.status === 200)) {
                    localStorage.setItem("token", `Bearer ${response.data}`);
                }
            });
        });
    }
}
