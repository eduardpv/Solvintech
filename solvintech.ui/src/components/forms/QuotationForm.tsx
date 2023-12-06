import { Button } from "../Button"
import { useEffect, useState } from "react";

import QuotationService from "../../services/QuotationService";
import AccountStore from "../../stores/AccountStore";
import Cookies from "universal-cookie";
import { Input } from "../Input";

interface IQuotationFormProps {
    accountStore: AccountStore
}

export const QuotationForm: React.FunctionComponent<IQuotationFormProps> = (props) => {
    const cookies = new Cookies();
    const accountStore = props.accountStore;
    const accessToken = accountStore.data.accessToken || cookies.get('accessToken');

    const [date, setDate] = useState("");

    function onDateChange(e: React.FormEvent<HTMLInputElement>) {
        setDate(e.currentTarget.value);
    }

    function onSubmit(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        QuotationService.get({
            date: date,
            headers: {
                accessToken: accessToken
            }
        })
            .then((response: any) => {
                console.log(response);
                alert("Data retrieved, check console.")
            })
            .catch((e: any) => {
                console.log(e.response);
            });
    }

    return (
        <form>
            <Input type="date" name="date" onChange={onDateChange} />
            <br />
            <Button name="Get quotations" onClick={onSubmit} />
        </form>
    )
}