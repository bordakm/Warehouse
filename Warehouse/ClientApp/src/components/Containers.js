import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';

export class Containers extends Component {
    static displayName = Containers.name;

    constructor(props) {
        super(props);
        this.state = { containers: [], loading: true, creatingContainer: false };
        this.handleContainerClick = this.handleContainerClick.bind(this);
    }

    componentDidMount() {
        this.populateContainersData();
    }

    handleContainerClick(id){
        this.props.history.push('/containers/' + id);
    } 

    renderContainersTable(containers) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Container name</th>
                        <th>Stored item count</th>
                        <th>Item names</th>
                        <th>Last modifying employee</th>
                    </tr>
                </thead>
                <tbody>                    
                    {containers.map(cont =>
                        <tr onClick={() => { this.handleContainerClick(cont.id)}} key={cont.id} >
                            <td >{cont.name}</td>
                            <td>{cont.storedItemCount}</td>
                            <td>{cont.itemsNames === "" ? "-" : cont.itemsNames}</td>
                            <td>{cont.lastEmployee ?? "-"}</td>
                        </tr>
                    )}
                </tbody>

                
            </table>
        );
    }

    newContainerClick = () => {
        this.setState({ creatingContainer: true });
    }

    handleNewContainer = async () => {
        const nameField = document.getElementById('newNameInput');
        const newContainerName = nameField.value;
        this.setState({ creatingContainer: false });
        if (newContainerName == "") return;
        nameField.value = "";

        const token = await authService.getAccessToken();
        const url = 'api/storage/containers/';
        const headers = { 'Content-Type': 'application/json', 'accept': 'text/plain' };
        if (token) headers.Authorization = `Bearer ${token}`;
        const bodydata = {
            id: -1,
            name: newContainerName
        };
        const response = await fetch(url, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(bodydata)
        });
        const data = await response.json();
        
        this.populateContainersData();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderContainersTable(this.state.containers);

        let nameInput;
        if (this.state.creatingContainer) {
            nameInput =
                <span>
                    <input type="text" className="form-control" id="newNameInput" placeholder="new container name" />
                    <button onClick={this.handleNewContainer} type="button" className="btn btn-primary mb-1 ml-4">Ok</button>
                </span>
        }
        else {
            nameInput = <button type="button" onClick={this.newContainerClick} className="btn btn-primary mb-1 ml-4">Add new container</button>
        }

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4 pagetitle">Containers</span>
                        <Link to="/items">
                            <button type="button" className="btn btn-primary mb-3 ml-4">Items</button>
                        </Link>
                        <button type="button" className="btn btn-secondary mb-3 mr-2">Containers</button>
                    </span>
                    <span className="mb-2">
                        {nameInput}
                        <Link to="/logs">
                            <button type="button" className="btn btn-primary mb-1 ml-3">Logs</button>
                        </Link>
                    </span>
                </div>

                {contents}
            </div>
        );
    }



    async populateContainersData() {
        const token = await authService.getAccessToken();
        let url = 'api/storage/containers';
        const response = await fetch(url, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ containers: data, loading: false });
    }
}
