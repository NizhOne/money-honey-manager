import * as React from "react";
import { ClipLoader } from 'react-spinners';

import Modal from "../Modal";
import "./spinner.css";

const Spinner = () =>
    <Modal>
        <div className="spinner">
            <ClipLoader
                sizeUnit={"px"}
                size={100}
            />
        </div>
    </Modal>;

export default Spinner;
