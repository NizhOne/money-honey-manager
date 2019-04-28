export const AUTH = 'AUTH';
export const REGISTER = 'REGISTER';
export const AUTH_SUCCESS = AUTH + '_SUCCESS';
export const AUTH_FAIL = AUTH + '_FAIL';

export function auth(payload) {
    return {
        type: AUTH,
        payload: {
            isLoading: true,
            ...payload
        }
    }
}

export function register(payload) {
    return {
        type: REGISTER,
        payload: {
            isLoading: true,
                ...payload
        }
    }
}

export function authSuccess() {
    return {
        type: AUTH_SUCCESS,
        payload: {
            isLoading: false,
            isAuthenticated: true
        }
    }
}

export function authFail(error) {
    return {
        type: AUTH_FAIL,
        payload: {
            isLoading: false,
            error
        }
    }
}
