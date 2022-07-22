import React, { Component } from 'react';
import { Link } from "react-router-dom";
import Services from '../../Services/ImplementedServices'

export class Navbar extends Component {
    static displayName = Navbar.name;

    constructor(props) {
        super(props)

        this.Links = this.Links.concat(props.Links)
    }

    Links = []

    GetLinks() {
        return this.Links.map(l => {
            return <Link to={ `/${l.link}` } className="navItem" onClick={ l.onClick }>{l.text}</Link>
        }) 
    } 

    OnTitleClick = () => { }

    Style = (new Services["StyleGen"]()).AddStyle({
            name: "nav",
            className: ".nav",
            style: () => [
                ["background-color", "white"],
                ["position", "sticky"],
                ["top", "0px"],
                ["width", "100%"],
                ["height", "50px"],
            ]
        })
        .AddStyle({
                name: "navItem",
                className: ".navItem",
                style: () => [
                    ["padding", "10px 5px"],
                    ["border-radius", "6px"],
                    ["background-color", "#37a6fd"],
                    ["margin", "auto 5px"],
                    ["height", "35px"],
                    ["display", "grid"],
                    ["place-content", "center"],
                    ["text-decoration", "unset"],
                    ["color", "unset"],
                ]
        })

        .AddStyle({
                name: "navItem",
                className: ".navItem:hover",
                style: () => [
                    ["filter","brightness(1.3)"]
                ]
        })

        .AddStyle({
                name: "siteTitle",
                className: ".siteTitle",
                style: () => [
                    ["padding", "10px 5px"],
                    ["display", "grid"],
                    ["place-content","center"]
                ]
            })

    render() {
        return (
            [
                <div className="nav">
                    <div className="siteTitle">Kanban React</div>
                    {[...this.GetLinks()]}
                </div>,
                <style>
                    {this.Style.GetStyles()}
                </style>
            ]
        );
    }
}