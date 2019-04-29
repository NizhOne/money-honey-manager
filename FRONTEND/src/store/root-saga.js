import { all, fork } from 'redux-saga/effects';
import authenticationSaga from "./sagas/authentication-saga";

export default function* rootSaga() {
    yield all([
        fork(authenticationSaga),
    ]);
}
