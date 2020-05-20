import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';

export class Items extends Component {
    static displayName = Items.name;

    constructor(props) {
        super(props);
        this.state = { items: [], loading: true, searchWord: "" };
    }

    componentDidMount() {
        this.populateItemsData();
    }

    handleSearchChange = (event) => {
        this.setState({ searchWord: event.target.value });
        this.populateItemsData();
    }


    static renderItemsTable(items) {
        if (items.length > 0) {
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
        }
        else {
            return (
                <div>
                    <hr />
                    <h4>There are no items to list!</h4>
                </div>
            );
        }
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Items.renderItemsTable(this.state.items);

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4">Items</span>
                        <button type="button" className="btn btn-secondary mb-3 ml-4">Items</button>
                        <Link to="/containers">
                            <button type="button" className="btn btn-primary mb-3 mr-2">Containers</button>
                        </Link>




                    </span>
                    <span className="mb-2">
                        <span className="pb-3">
                            <span> Filter items: </span>
                            <input onChange={this.handleSearchChange} type="text" className="form-control" id="filterText" placeholder="filter" />
                        </span>
                        <button type="button" className="btn btn-primary mb-1 mx-2">Add new item</button>
                    </span>
                </div>

                {contents}
            </div>
        );
    }



    async populateItemsData() {
        const token = await authService.getAccessToken();
        let url = 'api/storage/items';
        if (this.state.searchWord) url += "?search=" + this.state.searchWord;
        const response = await fetch(url, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ items: data, loading: false });
    }
}
