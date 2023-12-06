import { observer } from 'mobx-react-lite';
import { Button } from "../Button"
import { useEffect, useState } from "react";
import Cookies from 'universal-cookie';

import AccountStore from "../../stores/AccountStore";
import TokenService from '../../services/TokenService';

interface ITokenFormProps {
    accountStore: AccountStore
}

const TokenForm: React.FunctionComponent<ITokenFormProps> = (props) => {
    const cookies = new Cookies();
    const accountStore = props.accountStore;
    const accessToken = accountStore.data.accessToken || cookies.get('accessToken');
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

                cookies.set('accessToken', accessToken);
                accountStore.updateAccessToken(accessToken);
                onChangeToken(accessToken);

                alert(response.data.message);
            })
            .catch((e: any) => {
                console.log(e.response);
            });
    }

    return (
        <form>
            <p style={{ wordWrap: "break-word" }}>{token}</p>
            <br />
            <Button name="Update token" disabled={isDisabled()} onClick={onSubmit} />
        </form>
    )
}

export default observer(TokenForm);