import {useMemo} from "react";
import {Client} from "./client";

const useBffClient = () => {
    return useMemo<IApiClient>(() => {
        const baseUrl = process.env.REACT_APP_API_BASE_URL || "";
        const options = { fetch: window.fetch.bind(window) };

        return new DecoratedApiClient(baseUrl, options);
    }, []);
}

interface IApiClient extends Client {
    getContent: (path: string) => Promise<Blob>;
    
    getContentPath: (path: string) => string;
}

class DecoratedApiClient extends Client implements IApiClient {
    async getContent(path: string): Promise<Blob> {
        const url = new URL(`${this.baseUrl}/content/${path}`);
        const response = await this.http.fetch(url.toString());

        if (!response.ok)
            throw new Error(`Error fetching content: ${response.statusText}`);

        return response.blob();
    }
    
    getContentPath(path: string) : string {
        return `${this.baseUrl}/content/${path}`
    }
}

export default useBffClient;