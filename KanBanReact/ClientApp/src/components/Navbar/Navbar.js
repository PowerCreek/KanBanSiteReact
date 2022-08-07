import React, { Component } from 'react';
import { Link } from "react-router-dom";
import Services from '../../Services/ImplementedServices'

const NAV_CLASS = "nav-group"

export default function Navbar(props) {
    var Links = [].concat(props.Links)

    var GetLinks = () => {
        return Links.map(l => {
            return <Link to={`/${l.link}`} className="navItem" onClick={l.onClick}>{l.text}</Link>
        })
    }

    var Style = (new Services["StyleGen"]())
        .AddStyle({
            name: NAV_CLASS,
            className: `.${NAV_CLASS}`,
            style: () => [
                ["background-color", "white"],
                ["position", "sticky"],
                ["top", "0px"],
                ["width", "100%"],
                ["height", "50px"],
                ["display", "flex"],
                ["flex-direction", "row"],
                ["flex-wrap", "nowrap"],
                ["white-space", "nowrap"],
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
                ["filter", "brightness(1.3)"]
            ]
        })
        .AddStyle({
            name: "siteTitle",
            className: ".siteTitle",
            style: () => [
                ["padding", "10px 5px"],
                ["display", "grid"],
                ["place-content", "center"]
            ]
        })

    return (
        [
            <div className={NAV_CLASS}>
                <div className="siteTitle">Kanban React</div>
                {[...GetLinks()]}
            </div>,
            <style>
                {Style.GetStyles()}
            </style>
        ]
    );
}