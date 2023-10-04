import React, {createContext, FC, ReactNode, useContext} from "react";
import useBffClient from "../../hooks/useBffClient";

export interface UrlPreviewProps {
    url: string
    title: string | null
    iconUrl: string
    imageUrl: string
    summary: string | null
}

const UrlPreview = (props : UrlPreviewProps) => {
    const { title, summary, imageUrl, iconUrl } = props;
    
    return <>

        <pre>{JSON.stringify(props, null, 2)}</pre>
        <p>Title: {title}</p>
        <p>Summary: {summary}</p>
        {iconUrl && <img src={iconUrl} alt="Icon" />}
        {imageUrl && <img src={imageUrl} alt="Image Preview" />}
    </>
}

export default UrlPreview;

//const Context = createContext({});

// export const use = () => useContext(Context);
// export const Provider: FC<{ children?: ReactNode | undefined }> = props => {
//     return (
//         <Context.Provider value={{}}>
//             {props.children}
//         </Context.Provider>
//     );
// };

