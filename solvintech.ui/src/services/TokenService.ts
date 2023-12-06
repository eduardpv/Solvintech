import http from "../http/http-common";
import { ITokenCredentials } from "./interfaces/TokenCredentials";

const updateToken = (data: ITokenCredentials) => {
    const config = {
        headers: {
            "Authorization": `Bearer ${data.headers.accessToken}`
        }
    }

    return http.post<ITokenCredentials>("/token/generate", null, config);
}

const TokenService = {
    updateToken
};

export default TokenService;