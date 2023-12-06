export interface ISignUpCredentials {
    email: string;
    password: string;
    confirmPassword: string;
}

export interface ISignInCredentials {
    email: string;
    password: string;
}

export interface ILogoutCredentials {
    accessToken: string;
}