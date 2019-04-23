import * as React from "react";
import { Input, Col, Button } from "react-materialize";

import Container from "../Container";
import "./form.css"

class Form extends React.Component{
    componentWillMount() {
        this.initState();
    }

    initState = () => {
        const initState = {};
        this.props.fields.forEach(item => {
            initState[item.name] = "";
            initState[item.name + "Dirty"] = false;
        });
        this.setState({...initState});
    };

    validateEmail = (email) => {
        const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    };

    validateField = (fieldName = "text", value) => {
        if (value.length === 0) {
            return "This field is required"
        }
        switch (fieldName) {
            case "email":
                if (!this.validateEmail(value)) {
                    return "Please, enter correct email"
                }
                return "";
            case "password":
                if (value.length < 6) {
                    return "Password is too short"
                }
                return "";
            case "password_approve":
                if (value !== this.state.password) {
                    return "Passwords is different"
                }
                return "";
            case "name":
                if (value.length < 2) {
                    return "Name is too short";
                }
                return "";
            default:
                return "";
        }
    };

    handleInputChange = (event) => {
        const {target: {name, value, type, checked}} = event;
        const pureValue = type === 'checkbox' ? checked : value;
        this.setState({
            [name]: pureValue
        });
    };

    submitClick = () => {
        const initState = {};
        this.props.fields.forEach(item => {
            initState[item.name + "Dirty"] = true;
        });
        this.setState({...initState});
        const isValidate = this.props.fields.some(item => {
            return !this.validateField(item.type, this.state[item.name]);
        });
        if (isValidate) {
            this.props.getValues(this.state);
        }
    };

    render() {
        const { fields, buttonLabel, buttonChild, formHeader, submitError } = this.props;

        return (
            <Container className={["form"]}>
                <header className="form__header">{formHeader && formHeader.toUpperCase()}</header>
                <form className="form__wrapper">
                    <Col className="form__input">
                        {
                            fields.map(item =>
                                <div className="form__input_wrapper" key={item.name}>
                                    <Input
                                        {...item}
                                        onBlur={() => this.setState({[item.name + "Dirty"]: true})}
                                        value={this.state[item.name]}
                                        onChange={(event) => this.handleInputChange(event, item.type)}
                                        key={item.name}
                                        error={this.state[item.name + "Dirty"] ? this.validateField(item.name, this.state[item.name]) : ""}
                                    />
                                </div>)
                        }
                    </Col>
                    <div className="form__buttons">
                        <div className="form__buttons_inner">
                            <Button
                                type="button"
                                onClick={this.submitClick}
                                waves="light"
                            >
                                {buttonLabel}
                            </Button>
                            {submitError && <span className="form__buttons_error">Response error</span>}
                        </div>
                        {buttonChild}
                    </div>
                </form>
            </Container>
        );
    }
}

export default Form;
