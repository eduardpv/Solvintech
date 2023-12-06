import React from 'react';
import { observer } from 'mobx-react-lite';

import AccountStore from './stores/AccountStore';

import { SignUpForm } from './components/forms/SignUpForm';
import { QuotationForm } from './components/forms/QuotationForm';
import SignInForm from './components/forms/SignInForm';
import LogoutForm from './components/forms/LogoutForm';
import TokenForm from './components/forms/TokenForm';

interface IAppProps {
  accountStore: AccountStore
}

const App: React.FunctionComponent<IAppProps> = (props) => (
  <>
    <div>
      <h2>Register</h2>
      <SignUpForm />
    </div>
    <hr />
    <div>
      <h2>Log In</h2>
      <SignInForm accountStore={props.accountStore} />
    </div>
    <hr />
    <div>
      <h2>API Token</h2>
      <TokenForm accountStore={props.accountStore} />
    </div>
    <hr />
    <div>
      <h2>Log Out</h2>
      <LogoutForm accountStore={props.accountStore} />
    </div>
    <hr />
    <div>
      <h2>Quotations</h2>
      <QuotationForm accountStore={props.accountStore} />
    </div>

  </>
)

export default observer(App);