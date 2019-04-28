import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import Page from "../../components/Page";
import { loginUser } from "../../../modules/auth";
import LoginForm from "../../containers/AuthFormContainer";

const Login = props => (
    <Page id="login" title="Login" description="We need to log in to stuff.">
        <LoginForm />
        <button
            onClick={() => props.loginUser("user@mydomain.com", "password123")}
        >
            Click the button...
        </button>
    </Page>
);

const mapDispatchToProps = dispatch =>
    bindActionCreators({ loginUser }, dispatch);

export default connect(
    null,
    mapDispatchToProps
)(Login);
