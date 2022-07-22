
import Services from './ServiceCollection'
import StyleGen from './StyleGen'
import Workspaces from './Workspaces'

Services["StyleGen"] = StyleGen

Services["MainStyles"] = {
    "BasePage": {
        name: "page",
        className: ".basePage",
        style: () => [
            ["background-color", "gray"],
            ["position", "absolute"],
            ["width", "100%"],
            ["height", "100%"],
        ]
    }
}

Services["NavbarLinks"] = [
    {
        text: "Home",
        link: "home"
    },
    {
        text: "Boards",
        link: "dashboard"
    }
]

Services["DashboardSections"] = {
    Section1: ()=><div>section1</div>,
    Section2: ()=><div>section2</div>,
}

Services["Workspaces"] = new Workspaces()


export default Services;