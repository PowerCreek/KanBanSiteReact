import React, { useEffect, useState } from "react";
import Services, { WORKSPACES } from '../../Services/ImplementedServices'
import { Navbar } from '../Navbar/Navbar'


const DASHBOARD_SECTIONS_FUNC = "DashboardSectionsFunc"

Services[DASHBOARD_SECTIONS_FUNC] = (wk) => {

    let spaces = {}
    let AddBoard = (item) => {
        let arr = spaces[item.workspace] ??= []
        arr.push(item)
    }

    for(let w of wk){
        AddBoard(w)
    }

    let arr = {}
    for (let a of Object.entries(spaces)) {
        arr[a[0]] = <div className={BOARD_HEADER}>{a[0]}</div>
        arr[a[0] + "_"] = build =>
            <div className={BOARD_SEGMENT}>{[...build(a[1].flatMap(a => { delete a.workspace; return a })),
            <div className={`${BOARD_SEGMENT_CARD} ${BOARD_CREATE}`}><div className={BOARD_SEGMENT_CARD_CONTENT}>{
                "Create new board"
            }</div></div>]}</div>
    }

    return arr
}

const DASHBOARD_SECTIONS = "dashboard-sections"
Services["MainStyles"][DASHBOARD_SECTIONS] = {
    name: DASHBOARD_SECTIONS,
    className: `.${DASHBOARD_SECTIONS}`,
    style: [
        ["margin", "10px"],
        ["display", "flex"],
        ["flex-direction", "column"],
    ]
}

const BOARD_HEADER = "segment-header"
Services["MainStyles"][BOARD_HEADER] = {
    name: BOARD_HEADER,
    className: `.${BOARD_HEADER}`,
    style: [
        ["width", "100%"],
        ["border", "1px solid white"],
        ["margin", "5px 0px"],
        ["padding", "5px"],
        ["border-radius", "6px"],
    ]
}

const BOARD_SEGMENT = "segment"
Services["MainStyles"][BOARD_SEGMENT] = {
    name: BOARD_SEGMENT,
    className: `.${BOARD_SEGMENT}`,
    style: [
        ["width", "100%"],
        ["height", "min-content"],
        ["display", "grid"],
        ["overflow-x","hidden"],
        ["gap", "4px 10px"],
        ["grid-auto-flow", "dense row"],
        ["grid-template-columns", "repeat(auto-fill, minmax(200px, 1fr))"],
        ["place-content", "center"],
    ]
}

const BOARD_SUBSEGMENT = "sub-segment"
Services["MainStyles"][BOARD_SUBSEGMENT] = {
    name: BOARD_SUBSEGMENT,
    className: `.${BOARD_SEGMENT} > *`,
    style: [
        ["padding", "5px 10px"],
        ["border-radius", "6px"],
        ["background-color", "white"],
        ["width","min-content"],
        ["height","min-content"]
    ]
}
const BOARD_SEGMENT_CARD = "board-card"
const BOARD_SEGMENT_CARD_CONTENT = "board-card-content"

Services["MainStyles"][BOARD_SEGMENT_CARD] = {
    name: BOARD_SEGMENT_CARD,
    className: `.${BOARD_SEGMENT_CARD}`,
    style: [
        ["background-color", "white"],
        ["min-width", "200px"],
        ["min-height", "100px"],
        ["width", "100%"],
        ["height", "100px"],
        ["display", "flex"],
        ["flex-direction", "column"],
        ["justify-content","space-between"]
    ]
}

const BOARD_CREATE = "create"
Services["MainStyles"][BOARD_CREATE] = {
    name: BOARD_CREATE,
    className: `.${BOARD_SEGMENT_CARD}.${BOARD_CREATE}`,
    style: [
        ["justify-content","center"],
        ["align-items", "center"],
        ["background-color", "unset"],
        ["box-shadow", "inset 0 0 5px 0px white"],
        ["transition","150ms ease"]
    ]
}

const BOARD_CREATE_HOVER = "create_hover"
Services["MainStyles"][BOARD_CREATE_HOVER] = {
    name: BOARD_CREATE_HOVER,
    className: `.${BOARD_SEGMENT_CARD}.${BOARD_CREATE}:hover`,
    style: [
        ["box-shadow", "inset 0 0 10px 5px white"],
    ]
}

Services["MainStyles"][BOARD_SEGMENT_CARD_CONTENT] = {
    name: BOARD_SEGMENT_CARD_CONTENT,
    className: `.${BOARD_SEGMENT_CARD_CONTENT}`,
    style: [
    ]
}

function BuildSections(sectionsFunc) {
    return <div className={ DASHBOARD_SECTIONS }>{
        [
            ...sectionsFunc()
        ]
    }</div>
}

function constructBoardCard(data) {
    return <div className={BOARD_SEGMENT_CARD}>{
        [
            ...Object.entries(data).map(a =>
            <div className={BOARD_SEGMENT_CARD_CONTENT}>{a[1]}</div>)
        ]
    }</div>
}

export default function Dashboard() {
    let Style = (new Services["StyleGen"]())
        .AddStyle(Services["MainStyles"]["BasePage"])
        .AddStyle(Services["MainStyles"][DASHBOARD_SECTIONS])
        .AddStyle(Services["MainStyles"][BOARD_HEADER])
        .AddStyle(Services["MainStyles"][BOARD_SEGMENT])
        .AddStyle(Services["MainStyles"][BOARD_SUBSEGMENT])
        .AddStyle(Services["MainStyles"][BOARD_SEGMENT_CARD])
        .AddStyle(Services["MainStyles"][BOARD_SEGMENT_CARD_CONTENT])
        .AddStyle(Services["MainStyles"][BOARD_CREATE])
        .AddStyle(Services["MainStyles"][BOARD_CREATE_HOVER])

    const [value, setValue] = useState(1);

    useEffect(() =>
    {
        Services[WORKSPACES].GetWorkspacesAsync(data => {
            setValue((value + 1))
        })
    },
    [])

    let buildWorkspaces = (workspaces) => {
        return workspaces.map(wk => constructBoardCard(wk))
    }

    let mapWorkspace = a => {
        if ((typeof a[1]) == "function")
            return a[1](buildWorkspaces)
        return a[1]
    }

    let Sections = () => Object
        .entries(Services[DASHBOARD_SECTIONS_FUNC](
            Services[WORKSPACES].Workspaces
        ))
        .map(mapWorkspace)

    return (
        [
            <div className="basePage">
                <Navbar Links={[
                    ...Services["NavbarLinks"]
                ]} />
                <div>Dashboard</div>
                {BuildSections(Sections)}
            </div>,
            <style>
                {Style.GetStyles()}
            </style>
        ]
    )
}