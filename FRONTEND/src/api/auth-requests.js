import { callServer } from "../utils/request-util";
import { apiUrl } from "../utils/constants";

export default class AuthRequests {
    static getHeaders() {
        return {
            "content-type": "application/json"
        };
    }

    registration(name, email, password) {
        return new Promise((resolve, reject) => {
            callServer({
                url: apiUrl + "Auth/Register",
                method: "POST",
                headers: AuthRequests.getHeaders(),
                data: {
                    name,
                    email,
                    password
                }
            }).then(
                response => {
                    resolve(response)
                },
                error => {
                    // TODO add handling errors
                    console.log(error.response);
                    reject(error.response);
                });
        });
    }

    authorization(email, password) {
        return new Promise((resolve, reject) => {
            callServer({
                url: apiUrl + "Auth/Login",
                method: "POST",
                headers: AuthRequests.getHeaders(),
                data: {
                    email,
                    password
                }
            }).then(
                response => {
                    resolve(response);
                },
                error => {
                    // TODO add handling errors
                    console.log(error.response);
                    reject(error.response);
                }
            );
        });
    }
}
