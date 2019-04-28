import { AUTH, AUTH_FAIL, AUTH_SUCCESS } from "./authentication-actions";

const initState = {
    isLoading: false,
    isAuthenticated: false,
    error: ''
};

export default function authentication(state = initState, action) {
    switch (action.type) {
        case AUTH:
        case AUTH_SUCCESS:
        case AUTH_FAIL:
            return {
                ...initState,
                ...action.payload
            };
        default:
            return { ...initState };
    }
}
