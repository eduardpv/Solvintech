import { observer } from 'mobx-react-lite';
import { useEffect, useState } from "react";

import Constants from '../../common/constants';
import Cookies from 'universal-cookie';

import AccountStore from "../../stores/AccountStore";
import TokenService from '../../services/TokenService';

import { Form } from '../Form';
import { Button } from "../Button"
import { TokenInfo } from './elements/TokenInfo';

interface ITokenFormProps {
    accountStore: AccountStore
}

const TokenForm: React.FunctionComponent<ITokenFormProps> = (props) => {
    const cookies = new Cookies();
    const accountStore = props.accountStore;
    const accessToken = accountStore.data.accessToken || cookies.get(Constants.Common.AccessToken);
    const [token, setToken] = useState(accessToken);

    useEffect(() => {
        setToken(accessToken);
    }, [accountStore.data.accessToken])

    function onChangeToken(token: string) {
        setToken(token);
    }

    function isDisabled(): boolean {
        return !(accessToken) ? true : false;
    }

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const cookies = new Cookies();

        TokenService.updateToken({
            headers: {
                accessToken: accessToken
            }
        })
            .then((response: any) => {
                const accessToken = response.data.accessToken;

                cookies.set(Constants.Common.AccessToken, accessToken);
                accountStore.updateAccessToken(accessToken);
                onChangeToken(accessToken);

                alert(response.data.message);
            })
            .catch((e: any) => {
                console.log(e.response);
            });
    }

    return (
        <Form>
            <TokenInfo accessToken={token} />
            <br />
            <Button
                name={Constants.HtmlNamesDeclarations.UpdateToken}
                disabled={isDisabled()} onClick={onSubmit} />
        </Form>
    )
}

export default observer(TokenForm);