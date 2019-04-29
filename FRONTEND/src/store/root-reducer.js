import { combineReducers } from "redux";

import auth from "../modules/auth";
import profile from "../modules/profile";
import authentication from "./reducers/authentication/authentication-reducer";

export default combineReducers({
    auth,
    profile,
    authentication,
});
