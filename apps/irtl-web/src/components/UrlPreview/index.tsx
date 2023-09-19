import React, {createContext, FC, ReactNode, useContext} from "react";

interface UrlPreviewProps {
    url: string
}

const UrlPreview = () => {
    
}

export default UrlPreview;

const Context = createContext({});


export const use = () => useContext(Context);
export const Provider: FC<{ children?: ReactNode | undefined }> = props => {
    return (
        <Context.Provider value={{}}>
            {props.children}
        </Context.Provider>
    );
};

