import React, { Component } from 'react';
import Services from '../../Services/ImplementedServices'
import { Navbar } from '../Navbar/Navbar'

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props)
    }

    Style = (new Services["StyleGen"]()).AddStyle(
        Services["MainStyles"]["BasePage"]
    )

    render() {
        return (
            [
                <div className="basePage">
                    <Navbar Links={[
                        ...Services["NavbarLinks"]
                    ]} />
                    <div>Hello world!</div>
                </div>,
                <style>
                    {this.Style.GetStyles()}
                </style>
            ]
        );
    }
}