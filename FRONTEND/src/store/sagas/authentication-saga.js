import { call, put, takeEvery } from 'redux-saga/effects'

import AuthRequests from "../../api/auth-requests";
import { AUTH, authFail, authSuccess, REGISTER } from "../reducers/authentication/authentication-actions";

const api = new AuthRequests();

function* authentication(action) {
    try {
        const requestResult =
            yield call([api, "authorization"], action.payload.email, action.payload.password);
        if (requestResult.status === 200) {
            localStorage.setItem("token", `Bearer ${requestResult.data}`);
            yield put(authSuccess());
        }
    } catch (e) {
        yield put(authFail(e.statusText))
    }
}

function* registration(action) {
    try {
        const requestResult =
            yield call([api, "registration"], action.payload.name, action.payload.email, action.payload.password);
        if (requestResult.status === 200) {
            localStorage.setItem("token", `Bearer ${requestResult.data}`);
            yield put(authSuccess());
        }
    } catch (e) {
        yield put(authFail(e.data[0].message))
    }
}

export default function* authenticationSaga() {
    yield takeEvery(AUTH, authentication);
    yield takeEvery(REGISTER, registration)
}
