import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';

export class Users extends Component {
    static displayName = Users.name;

    constructor(props) {
        super(props);
        this.state = { users: [], loading: true };
    }

    componentDidMount() {
        this.populateUserData();
    }

    renderForecastsTable(users) {
        console.log(users)
        return (
            <table className='table' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Email address</th>
                        <th>Full name</th>
                        <th>User role</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(user =>
                        <tr key={user.id}>
                            <td>{user.email}</td>
                            <td>{user.fullName}</td>
                            <td>{user.role}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForecastsTable(this.state.users);

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4 pagetitle">Users</span>


                    </span>
                    <span className="mb-2">
                        <Link to="/users/new">
                            <button type="button" className="btn btn-primary mb-1 ml-3">Add new user</button>
                        </Link>
                    </span>
                </div>

                {contents}
            </div>
        );
    }

    async populateUserData() {
        const token = await authService.getAccessToken();
        const response = await fetch('api/users', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ users: data, loading: false });
    }
}
