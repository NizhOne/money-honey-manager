import { createStore, applyMiddleware, compose } from "redux";
import { connectRouter, routerMiddleware } from "connected-react-router";
import thunk from "redux-thunk";
import { createBrowserHistory, createMemoryHistory } from "history";
import rootReducer from "./root-reducer";
import createSagaMiddleware from "redux-saga";

import rootSaga from "./root-saga";

// A nice helper to tell us if we're on the server
export const isServer = !(
    typeof window !== "undefined" &&
    window.document &&
    window.document.createElement
);

export default (url = "/") => {
    // Create a history depending on the environment
    const history = isServer
        ? createMemoryHistory({
              initialEntries: [url]
          })
        : createBrowserHistory();

    const enhancers = [];

    // Dev tools are helpful
    if (process.env.NODE_ENV === "development" && !isServer) {
        const devToolsExtension = window.__REDUX_DEVTOOLS_EXTENSION__;

        if (typeof devToolsExtension === "function") {
            enhancers.push(devToolsExtension());
        }
    }

    const sagaMiddleware = createSagaMiddleware();
    const middleware = [thunk, routerMiddleware(history), sagaMiddleware];
    const composedEnhancers = compose(
        applyMiddleware(...middleware),
        ...enhancers
    );

    // Do we have preloaded state available? Great, save it.
    const initialState = !isServer ? window.__PRELOADED_STATE__ : {};

    // Delete it once we have it stored in a variable
    if (!isServer) {
        delete window.__PRELOADED_STATE__;
    }

    // Create the store
    const store = createStore(
        connectRouter(history)(rootReducer),
        initialState,
        composedEnhancers
    );

    sagaMiddleware.run(rootSaga);

    return {
        store,
        history
    };
};
