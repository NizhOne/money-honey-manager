import { connect } from "react-redux";
import AuthForm from "../../components/AuthForm/AuthForm";
import { auth, register } from "../../../store/reducers/authentication/authentication-actions";

const mapStateToProps = (state) => ({
    isLoading: state.authentication.isLoading,
    error: state.authentication.error,
    isAuthenticated: state.authentication.isAuthenticated
});

const mapDispatchToProps = dispatch => ({
    authorization: (email, password) => {
        dispatch(auth({email, password}))
    },
    registration: (name, email, password) => {
        dispatch(register({name, email, password}))
    }
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(AuthForm)
