import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';


export class NewUser extends Component {
    static displayName = NewUser.name;

    constructor(props) {
        super(props);
        this.state = { 
            user: {},
            roles:[],
            loading: true            
        };
    }

    componentDidMount() {        
        this.populateRolesData();
    }

    handleEmailChange = (event) => {
        var tmpUser = { ...this.state.user };
        tmpUser.email = event.target.value;
        this.setState({ user: tmpUser })
    };

    handleNameChange = (event) => {
        var tmpUser = { ...this.state.user };
        tmpUser.name = event.target.value;
        this.setState({ user: tmpUser })
    };

    handlePasswordChange = (event) => {
        var tmpUser = { ...this.state.user };
        tmpUser.password = event.target.value;
        this.setState({ user: tmpUser })
    };

    handleRoleChange = (event) => {
        var tmpUser = { ...this.state.user };
        tmpUser.role = event.target.value;
        this.setState({ user: tmpUser })
    };

    handleSubmitUser = async () => {

        if (!this.state.user.role) {
            var tmpUser = { ...this.state.user };
            tmpUser.role = this.state.roles[0]??"basic";
            this.setState({ user: tmpUser });
        }

        const token = await authService.getAccessToken();
        const url = 'api/users';
        const headers = { 'Content-Type': 'application/json', 'accept': 'text/plain' };
        if (token) headers.Authorization = `Bearer ${token}`;
        const bodydata = {
            Email: this.state.user.email,
            Password: this.state.user.password,
            FullName: this.state.user.name,
            Role: this.state.user.role
        };
        console.log(bodydata);
        await fetch(url, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(bodydata)
        }).then(() => { this.props.history.push('/users/'); });
    }

    renderItemData() {
        return (
            <div>
                <hr />
                <div>

                    <label htmlFor="emailInput">Email: </label>
                    <input type="text" value={this.state.user.email} onChange={this.handleEmailChange} className="form-control mb-3" id="emailInput" placeholder="Employee email address" />

                    <label htmlFor="fullnameInput">Full name: </label>
                    <input type="text" value={this.state.user.fullname} onChange={this.handleNameChange} className="form-control mb-3" id="fullnameInput" placeholder="Full name" />

                    <label htmlFor="passworInput">Password: </label>
                    <input type="password" value={this.state.user.password} onChange={this.handlePasswordChange} className="form-control mb-3" id="passworInput" placeholder="Enter password here" />

                   
                    <label htmlFor="roleInput">Role: </label>
                    <select className="form-control mb-3" value={this.state.role} onChange={this.handleRoleChange} id="roleInput" >
                        {this.state.roles.map(role =>
                            <option value={role} key={role}>{role}</option>
                        )}
                    </select>
                    <button className="btn btn-primary" onClick={this.handleSubmitUser}>Save changes</button>

                </div>
            </div>
        );
    }


    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderItemData();

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4">Create new user</span>
                    </span>
                </div>
                {contents}
            </div>
        );
    }

    async populateRolesData() {
        const token = await authService.getAccessToken();
        let url = 'api/users/roles';
        const response = await fetch(url, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ roles: data, loading:false });
    }
}
