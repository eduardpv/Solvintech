import http from "../http/http-common";
import { ISignUpCredentials, ISignInCredentials, ILogoutCredentials } from "./interfaces/AccountCredentials";

const signUp = (data: ISignUpCredentials) => {
    return http.post<ISignUpCredentials>("/account/signup", data);
}

const signIn = (data: ISignInCredentials) => {
    return http.post<ISignInCredentials>("/account/signin", data);
}

const logout = (data: ILogoutCredentials) => {
    return http.post<ILogoutCredentials>("/account/logout", data);
}

const AccountService = {
    signUp,
    signIn,
    logout
};

export default AccountService;