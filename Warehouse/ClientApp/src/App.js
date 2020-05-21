import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Items } from './components/Items';
import { Containers } from './components/Containers';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import './custom.css'
import { EditContainer } from './components/EditContainer';
import { EditItem } from './components/EditItem';
import { Logs } from './components/Logs';
import { Users } from './components/Users';
import { NewUser } from './components/NewUser';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <AuthorizeRoute exact path='/' component={Containers} />
                <AuthorizeRoute path='/counter' component={Counter} />
                <AuthorizeRoute path='/fetch-data' component={FetchData} />
                <AuthorizeRoute exact path='/items' component={Items} />
                <AuthorizeRoute exact path='/items/:id' component={EditItem} />
                <AuthorizeRoute exact path='/containers' component={Containers} />
                <AuthorizeRoute path='/containers/:id' component={EditContainer} />    
                <AuthorizeRoute path='/logs' component={Logs} />
                <AuthorizeRoute exact path='/users' component={Users} />
                <AuthorizeRoute exact path='/users/new' component={NewUser} />
                <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
            </Layout>
        );
    }
}
