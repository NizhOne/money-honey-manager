import * as React from "react";

import  "./container.css"

const Container = props => {
    const {className = [], children, type = "text"} = props;
    const concatClasses = (array) => array.join(" ");

    return (
        <div className={`container ${concatClasses(className)}`}>
            <div className={`container__${type}`}>
                {children}
            </div>
        </div>
    );
};

export default Container;
