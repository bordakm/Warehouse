import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Link } from 'react-router-dom';

export class Logs extends Component {
    static displayName = Logs.name;

    constructor(props) {
        super(props);
        this.state = { logs: [], loading: true};
    }

    componentDidMount() {
        this.populateLogsData();
    }

    deleteLogs = async () => {
        const token = await authService.getAccessToken();
        const url = 'api/logs';
        const headers = { 'Content-Type': 'application/json' };
        if (token) headers.Authorization = `Bearer ${token}`;
        const response = await fetch(url, {
            method: 'DELETE',
            headers: headers
        });

        this.populateLogsData();
    }

    renderLogsTable() {
        if (this.state.logs.length > 0) {
            return (
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Time</th>
                            <th>Employee name</th>
                            <th>Event</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.logs.map(log =>
                            <tr key={log.time}>
                                <td>{log.time}</td>
                                <td>{log.employeeName}</td>
                                <td>{log.text}</td>
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
                    <h4>There are no logs to list!</h4>
                </div>
            );
        }
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderLogsTable();

        return (
            <div>
                <div className="d-flex justify-content-between align-items-end">
                    <span>
                        <span className="display-4 pagetitle">Logs</span>
                    </span>
                    <span className="mb-2">
                        <button type="button" onClick={this.deleteLogs} className="btn btn-danger mb-1 mx-2">Delete all logs</button>
                    </span>
                </div>

                {contents}
            </div>
        );
    }



    async populateLogsData() {
        const token = await authService.getAccessToken();
        let url = 'api/logs';      
        const response = await fetch(url, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ logs: data, loading: false });
    }
}
