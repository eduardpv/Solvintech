import { observer } from 'mobx-react-lite';
import { useEffect, useState } from 'react';

import Constants from '../../common/constants';
import Cookies from 'universal-cookie';

import AccountService from "../../services/AccountService";
import AccountStore from "../../stores/AccountStore";

import { Button } from "../Button"
import { Form } from '../Form';

interface ILogoutFormProps {
    accountStore: AccountStore
}

const LogoutForm: React.FunctionComponent<ILogoutFormProps> = (props) => {
    const cookies = new Cookies();
    const accountStore = props.accountStore;
    const accessToken = accountStore.data.accessToken || cookies.get(Constants.Common.AccessToken);
    const [isDisabled, setIsDisabled] = useState(false);

    useEffect(() => {
        setIsDisabled(!!accessToken);
    }, [accountStore.data.accessToken])

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const cookies = new Cookies();

        AccountService.logout({
            headers: {
                accessToken: accountStore.data.accessToken || cookies.get(Constants.Common.AccessToken)
            }
        })
            .then((response: any) => {
                cookies.remove(Constants.Common.AccessToken);
                accountStore.updateAccessToken('');

                alert(response?.data?.message);
            })
            .catch((e: any) => {
                alert(`${e?.response?.statusText} - please, check console logs`);
                console.log(e?.response);
            });
    }

    return (
        <Form>
            <Button
                name={Constants.HtmlNamesDeclarations.LogOut}
                disabled={!isDisabled}
                onClick={onSubmit} />
        </Form>
    )
}

export default observer(LogoutForm);