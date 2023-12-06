import { observer } from 'mobx-react-lite';
import { FormGroup } from "../FormGroup"
import { Button } from "../Button"
import { useState } from "react";
import Cookies from 'universal-cookie';

import AccountService from "../../services/AccountService";
import AccountStore from "../../stores/AccountStore";

interface ISignInFormProps {
    accountStore: AccountStore
}

const SignInForm: React.FunctionComponent<ISignInFormProps> = (props) => {
    const accountStore = props.accountStore;

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    function onChangeEmail(e: React.FormEvent<HTMLInputElement>) {
        setEmail(e.currentTarget.value);
    }

    function onChangePassword(e: React.FormEvent<HTMLInputElement>) {
        setPassword(e.currentTarget.value);
    }

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        AccountService.signIn({
            email: email,
            password: password
        })
            .then((response: any) => {
                const accessToken = response.data.accessToken;

                const cookies = new Cookies();
                cookies.set('accessToken', accessToken);
                accountStore.updateAccessToken(accessToken);

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
            <br />
            <Button name="Log In" onClick={onSubmit} />
        </form>
    )
}

export default observer(SignInForm);