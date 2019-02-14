import * as React from "react";

import Form from "../../components/Form";
import { Button } from "react-materialize";

const formStub = [
    {
        name: "login",
        label: "Login",
        type: "text"
    },
    {
        name: "password",
        label: "Password",
        type: "password"
    }
];

class LoginForm extends React.Component {
    getValues = (values) => {
        console.log(values);
    } ;

    render() {
        const fbButton =
            <>
                <Button className="fb-color">Login with FB</Button>
                or
                <Button flat className="link">Register</Button>
            </>;

        return <Form
            fields={formStub}
            buttonLabel={"Login with email"}
            buttonChild={fbButton}
            getValues={this.getValues}
        />;
    }
}

export default LoginForm;

