import * as React from "react";
import { BeatLoader } from "react-spinners";
import { Button } from "react-materialize";

import "./custom-button.css";

const CustomButton = props =>
    <Button
        {
            ...props
        }
    >
        <div>
            <span>{props.children}</span>
            {
                props.disabled &&
                <div className="custom-button">
                    <BeatLoader
                        sizeUnit={"px"}
                        size={6}
                        color={"#fff"}
                    />
                </div>
            }
        </div>
    </Button>;

export default CustomButton;
