import { observer } from 'mobx-react-lite';
import { FormGroup } from "../FormGroup"
import { Button } from "../Button"
import { useState } from "react";
import Cookies from 'universal-cookie';

import AccountService from "../../services/AccountService";
import AccountStore from "../../stores/AccountStore";

interface ILogoutFormProps {
    accountStore: AccountStore
}

const LogoutForm: React.FunctionComponent<ILogoutFormProps> = (props) => {
    const accountStore = props.accountStore;

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const cookies = new Cookies();

        AccountService.logout({
            accessToken: accountStore.data.accessToken || cookies.get('accessToken')
        })
            .then((response: any) => {
                cookies.remove('accessToken');
                accountStore.updateAccessToken('');

                alert(response.data.message);
            })
            .catch((e: any) => {
                console.log(e.response);
            });
    }

    return (
        <form>
            <Button name="Log Out" onClick={onSubmit} />
        </form>
    )
}

export default observer(LogoutForm);