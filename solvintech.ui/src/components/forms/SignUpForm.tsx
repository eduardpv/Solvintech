import { FormGroup } from "../FormGroup"
import { Button } from "../Button"
import { useState } from "react";

import AccountService from "../../services/AccountService";

export const SignUpForm: React.FunctionComponent = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setComfirmPassword] = useState("");

    function onChangeEmail(e: React.FormEvent<HTMLInputElement>) {
        setEmail(e.currentTarget.value);
    }

    function onChangePassword(e: React.FormEvent<HTMLInputElement>) {
        setPassword(e.currentTarget.value);
    }

    function onChangeConfirmPassword(e: React.FormEvent<HTMLInputElement>) {
        setComfirmPassword(e.currentTarget.value);
    }

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        AccountService.signUp({
            email: email,
            password: password,
            confirmPassword: confirmPassword
        })
            .then((response: any) => {
                alert(response.data.message);
            })
            .catch((e: any) => {
                console.log(e.response);
            });
    }

    return (
        <form>
            <FormGroup type="email" name="Email" title="Email" onChange={onChangeEmail} />
            <FormGroup type="password" name="Password" title="Password" onChange={onChangePassword} />
            <FormGroup type="password" name="ConfirmPassword" title="Confirm Password" onChange={onChangeConfirmPassword} />
            <br />
            <Button name="Register" onClick={onSubmit} />
        </form>
    )
}