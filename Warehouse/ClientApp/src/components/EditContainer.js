import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';


export class EditContainer extends Component {
    static displayName = EditContainer.name;

    constructor(props) {
        super(props);
        this.state = {
            containerId: -1,
            container: {},
            items: [],
            changingName: false,
            loading: true
        };
    }

    componentDidMount() {
        this.setState({
            containerId: this.props.match.params.id
        });
        this.populateItemsData();
    }


    static renderItemsTable(items) {
        if (items.length > 0)
            return (
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Item name</th>
                            <th>Item description</th>
                            <th>Count</th>
                            <th>Container</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map(it =>
                            <tr key={it.id}>
                                <td>{it.name}</td>
                                <td>{it.description}</td>
                                <td>{it.count}</td>
                                <td>{it.containerName}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            );
        else
            return (
                <div>
                    <hr/>
                    <h4>This container does not contain any items!</h4>
                </div>
            );
    }


    handleNameChange = async () => {
        const newName = document.getElementById('newNameInput').value;
        
        const token = await authService.getAccessToken();
        const url = 'api/storage/containers/'; // + this.state.containerId
        const headers = { 'Content-Type': 'application/json', 'accept': 'text/plain' };
        if (token) headers.Authorization = `Bearer ${token}`;
        const bodydata = {
            id: parseInt(this.state.containerId),
            name: newName
        };
        const response = await fetch(url, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(bodydata)
        });
        const data = await response.json();
        this.setState({ changingName:false });
        this.populateItemsData();
    }

    nameChangeClick = ()=> {
        this.setState({ changingName: true });
    }

    deleteContainer = async () => {
        const token = await authService.getAccessToken();
        const url = 'api/storage/containers/' + this.state.containerId;
        const headers = { 'Content-Type': 'application/json', 'accept': 'text/plain' };
        if(token) headers.Authorization = `Bearer ${token}`;
        const bodydata = {
            id: parseInt(this.state.containerId)            
        };
        const response = await fetch(url, {
            method: 'DELETE',
            headers: headers,
            body: JSON.stringify(bodydata)
        }).then(() => { this.props.history.push('/containers')});
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : EditContainer.renderItemsTable(this.state.items);

        let nameChanger;
        if (this.state.changingName) {
            nameChanger =
                <span>
                    <input type="text" className="form-control" id="newNameInput" placeholder="new container name" />
                    <button onClick={this.handleNameChange} type="button" className="btn btn-primary mb-1 ml-4">Ok</button>
                </span>
        }
        else {
            nameChanger = <button type="button" onClick={this.nameChangeClick} className="btn btn-primary mb-1 ml-4">Rename container</button>
        }

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4">Container: {this.state.container.name}</span>
                    </span>
                    <span className="mb-2">
                        {nameChanger}                        
                        <button onClick={this.deleteContainer} type="button" className="btn btn-danger mb-1 ml-3">Delete container</button>
                    </span>
                </div>

                {contents}
            </div>
        );
    }



    async populateItemsData() {
        const token = await authService.getAccessToken();
        let url = 'api/storage/containers/' + this.state.containerId;
        const response = await fetch(url, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ items: data.items, loading: false });
        this.setState({ container: data, loading: false });
    }
}
