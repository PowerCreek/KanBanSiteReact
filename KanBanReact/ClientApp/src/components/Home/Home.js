import React, { useEffect, useState } from "react";
import Services from '../../Services/ImplementedServices'
import Navbar from '../Navbar/Navbar'

export default function Home() {
    let Style = (new Services["StyleGen"]())
        .AddStyle(Services["MainStyles"]["BasePage"])

    return (
        [
            <div className="basePage">
                <Navbar Links={[
                    ...Services["NavbarLinks"]
                ]} />
                <div>Hello world!</div>
            </div>,
            <style>
                {Style.GetStyles()}
            </style>
        ]
    )
}