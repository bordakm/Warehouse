import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';


export class EditItem extends Component {
    static displayName = EditItem.name;

    constructor(props) {
        super(props);
        this.state = {
            itemId: -1,
            item: {},
            editedItem: {},
            loading: true,
            containers: []
        };
    }

    componentDidMount() {
        this.setState({
            itemId: this.props.match.params.id
        });
        this.populateItemData();     
        this.populateContainersData();
    }

    handleNameChange = (event) => {
        var tmpItem = { ...this.state.editedItem };
        tmpItem.name = event.target.value;
        this.setState({ editedItem: tmpItem })
    };  

    handleDescChange = (event) => {
        var tmpItem = { ...this.state.editedItem };
        tmpItem.description = event.target.value;
        this.setState({ editedItem: tmpItem })
    }; 

    handleCountChange = (event) => {
        var tmpItem = { ...this.state.editedItem };
        tmpItem.count = event.target.value;
        this.setState({ editedItem: tmpItem })        
    }; 
    
    handleContainerChange = (event) => {
        var tmpItem = { ...this.state.editedItem };
        tmpItem.containerId = event.target.value;
        this.setState({ editedItem: tmpItem })
    };

    handleSubmitChanges = async () => {
        if (!this.state.editedItem.name || this.state.editedItem.name.length == null) return;
        const token = await authService.getAccessToken();
        const url = 'api/storage/items';
        const headers = { 'Content-Type': 'application/json', 'accept': 'text/plain' };
        if (token) headers.Authorization = `Bearer ${token}`;
        const id = this.state.editedItem.id;
        const bodydata = {
            id: parseInt(id==null?-1:id),
            name: this.state.editedItem.name,
            description: this.state.editedItem.description,
            count: parseInt(this.state.editedItem.count),
            containerId: parseInt(this.state.editedItem.containerId)
        };
        const response = await fetch(url, {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(bodydata)
        }).then(() => { this.props.history.push('/containers/' + this.state.editedItem.containerId); });
    }

    renderItemData() {
        return (
            <div>
                <hr />
                <div>

                    <label htmlFor="itemNameInput">Item name: </label>
                    <input type="text" value={this.state.editedItem.name} onChange={this.handleNameChange} className="form-control mb-3" id="itemNameInput" placeholder="item name here" />

                    <label htmlFor="itemDescInput">Item description: </label>
                    <textarea rows="3" type="text" value={this.state.editedItem.description} onChange={this.handleDescChange} className="form-control mb-3" id="itemDescInput" placeholder="item description here" />

                    <label htmlFor="itemCountInput">Item count: </label>
                    <input type="number" pattern="[0-9]" value={this.state.editedItem.count} onChange={this.handleCountChange} className="form-control mb-3" id="itemCountInput" />

                    <label htmlFor="itemContainerInput">Container: </label>
                    <select className="form-control mb-3" value={this.state.editedItem.containerId} onChange={this.handleContainerChange} id="itemContainerInput" >
                        {this.state.containers.map(cont =>
                            <option value={cont.id} key={cont.id}>{cont.name}</option>
                        )}
                    </select>
                    <button className="btn btn-primary" onClick={this.handleSubmitChanges}>Save changes</button>

                </div>
            </div>
        );
    }

    deleteItem = async () => {
        const token = await authService.getAccessToken();
        const url = 'api/storage/items/' + this.state.item.id;
        const headers = { 'Content-Type': 'application/json'};
        if(token) headers.Authorization = `Bearer ${token}`;
        const response = await fetch(url, {
            method: 'DELETE',
            headers: headers
        }).then(() => { this.props.history.push('/containers/' + this.state.item.containerId) });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderItemData();

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4">Item: {this.state.item.name}</span>
                    </span>
                    {this.state.item.containerId >=0 && <span className="mb-2">
                        <button onClick={this.deleteItem} type="button" className="btn btn-danger mb-1 ml-3">Delete item</button>
                    </span>}

                </div>
                {contents}
            </div>
        );
    }



    async populateItemData() {
        if (this.props.match.params.id == -1) {
            console.log("thisisnew")

            let tmp = { ...this.state.editedItem };
            tmp.containerId = this.props.location.data;
            tmp.count = 1;
            this.setState({ loading: false, editedItem: tmp });
            
            return;
        }
        const token = await authService.getAccessToken();
        let url = 'api/storage/items/' + this.state.itemId;
        const response = await fetch(url, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ item: data, loading: false, editedItem: data });
    }

    async populateContainersData() {
        const token = await authService.getAccessToken();
        let url = 'api/storage/containers';
        const response = await fetch(url, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ containers: data });
    }
}
