import * as React from "react";
import { Input, Col, Button } from "react-materialize";

import Container from "../Container";
import "./form.css"

class Form extends React.Component{
    // TODO add validation rules
    handleInputChange = (event) => {
        const {target: {name, value, type, checked}} = event;
        const pureValue = type === 'checkbox' ? checked : value;

        this.setState({
            [name]: pureValue
        });
    };

    render() {
        const { fields, buttonLabel, buttonChild } = this.props;

        return (
            <Container className={["form"]}>
                <form className="form__wrapper">
                    <Col>
                        {
                            fields.map(item =>
                                <Input
                                    {...item}
                                    value={(this.state && this.state[item.name]) || ""}
                                    onChange={this.handleInputChange}
                                    key={item.name}
                                />)
                        }
                    </Col>
                    <div className="form__buttons">
                        <Button
                            type="button"
                            onClick={() => this.props.getValues(this.state)}
                            waves="light"
                        >
                            {buttonLabel}
                        </Button>
                        {buttonChild}
                    </div>
                </form>
            </Container>
        );
    }
}

export default Form;
