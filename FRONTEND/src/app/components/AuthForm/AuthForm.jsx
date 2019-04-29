import * as React from "react";

import Form from "../Form/index";
import { Button } from "react-materialize";
import "./auth-form.css";

class AuthForm extends React.Component {
    state = {
        isRegistration: false
    };

    switchRegistration = () => {
        this.setState({
            isRegistration: !this.state.isRegistration
        })
    };

    getValues = (values) => {
        const {email, name, password} = values;
        this.state.isRegistration
            ? this.props.registration(name, email, password)
            : this.props.authorization(email, password);
    } ;

    render() {
        const authStub = [
            {
                name: "email",
                label: "E-mail",
                type: "email"
            },
            {
                name: "password",
                label: "Password",
                type: "password",
            }
        ];
        const registerStub = [
            {
                name: "name",
                label: "Name",
                type: "text"
            },
            ...authStub,
            {
                name: "password_approve",
                label: "Confirm password",
                type: "password",
            }
        ];
        const formStub = this.state.isRegistration ? registerStub : authStub;
        const isRegistrationText = isRegistration => isRegistration  ? "register": "login";
        const fbButton =
            <>
                {/*<Button className="fb-color">{isRegistrationText(this.state.isRegistration)} with FB</Button>*/}
                or
                <Button
                    flat
                    type="button"
                    className="link"
                    onClick={this.switchRegistration}
                >{isRegistrationText(!this.state.isRegistration)}</Button>
            </>;

        return <div className="auth-form">
            <Form
                fields={formStub}
                buttonLabel={isRegistrationText(this.state.isRegistration) + " with e-mail"}
                buttonChild={fbButton}
                getValues={this.getValues}
                key={formStub}
                isLoading={this.props.isLoading}
                error={this.props.error}
            />
        </div>
    }
}

export default AuthForm;

