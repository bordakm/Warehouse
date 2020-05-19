import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';


export class Containers extends Component {
    static displayName = Containers.name;

    constructor(props) {
        super(props);
        this.state = { containers: [], loading: true };
        this.handleContainerClick = this.handleContainerClick.bind(this);
    }

    componentDidMount() {
        this.populateContainersData();
    }

    handleContainerClick(event){
       //  console.log("asd", event.target.parentNode.getAttribute('data-id')); TODO
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
                        <tr onClick={this.handleContainerClick} data-id={cont.id} key={cont.id} >
                            <td >{cont.name}</td>
                            <td>{cont.storedItemCount}</td>
                            <td>{cont.itemsNames === "" ? "-" : cont.itemsNames}</td>
                            <td>{cont.lastEmployee ?? "-"}</td>
                            <td><button >asdd</button></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderContainersTable(this.state.containers);

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4">Containers</span>
                        <Link to="/items">
                            <button type="button" className="btn btn-primary mb-3 ml-4">Items</button>
                        </Link>
                        <button type="button" className="btn btn-secondary mb-3 mr-2">Containers</button>
                    </span>
                    <span className="mb-2">
                        <button type="button" className="btn btn-primary mb-1 mx-2">Add new container</button>
                        <button type="button" className="btn btn-primary mb-1 ml-3">Logs</button>
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
