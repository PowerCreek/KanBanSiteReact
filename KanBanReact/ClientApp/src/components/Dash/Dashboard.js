import React, { Component } from 'react';
import Services from '../../Services/ImplementedServices'
import { Navbar } from '../Navbar/Navbar'

export class Dashboard extends Component {
    static displayName = Dashboard.name;

    constructor(props) {
        super(props)
    }

    Style = (new Services["StyleGen"]()).AddStyle(
        Services["MainStyles"]["BasePage"]
    )

    Sections = ()=>Object.entries(Services["DashboardSections"]).map(a => {
        if ((typeof a[1]) == "function")
            return a[1]()
        return a[1]
    })

    render() {
        return (
            [
                <div className="basePage">
                    <Navbar Links={[
                        ...Services["NavbarLinks"]
                    ]} />
                    <div>Dashboard</div>
                    {[
                        ...this.Sections()
                    ]}
                </div>,
                <style>
                    {this.Style.GetStyles()}
                </style>
            ]
        );
    }
}