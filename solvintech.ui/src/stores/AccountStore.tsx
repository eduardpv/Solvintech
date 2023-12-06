import { makeObservable, observable } from 'mobx';

export default class AccountStore {
    data = {
        accessToken: ""
    }

    constructor() {
        makeObservable(this, {
            data: observable
        })
    }

    updateAccessToken = (accessToken: string) => {
        this.data.accessToken = accessToken;
    }
}