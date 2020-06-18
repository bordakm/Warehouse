import React, { Component } from 'react';

export class WarehouseTemperature extends Component {
    static displayName = WarehouseTemperature.name;
    constructor(props) {
        super(props);
        this.state = {
            fahreinheit: false,
            temp: {},
        };
    }

    componentDidMount() {
        this.populateTemperatureData();
    }

    changeUnit = () => {
        this.setState({ fahreinheit: !this.state.fahreinheit });
        this.populateTemperatureData();
    }

    render() {
        const tempdata = this.state.temp.temperature ? this.state.temp.temperature.toFixed(2) + this.state.temp.unit : "";
        return (
            <div className="d-flex" onClick={this.changeUnit}>
                <span style={{ fontSize: "12px", lineHeight: "13px", marginLeft: "10px", marginRight: "20px" }}>
                    <div>
                        Current
                        </div>
                    <div>
                        Temp:
                        </div>
                </span>
                <span>
                    {tempdata}
                </span>
            </div>
        );
    }

    async populateTemperatureData() {
        let unit = this.state.fahreinheit ? "f" : "c";
        let url = 'api/storage/temperature/' + unit;
        const response = await fetch(url, {});
        const data = await response.json();
        this.setState({ temp: data });
    }
}
