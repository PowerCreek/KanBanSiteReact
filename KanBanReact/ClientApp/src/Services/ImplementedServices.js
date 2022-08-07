
import Services from './ServiceCollection'
import StyleGen from './StyleGen'
import Workspaces from './Workspaces'
import FetchExt from './FetchExt'

export const WORKSPACES = "Workspaces"

export const API_URL = "ApiUrl"

let ApiEndpoint = "https://localhost:7007"

Services[API_URL] = ApiEndpoint

Services["FetchFunc"] = FetchExt

Services["StyleGen"] = StyleGen

Services["MainStyles"] = {
    BasePage: {
        name: "page",
        className: ".basePage",
        style: () => [
            ["background-color", "gray"],
            ["position", "absolute"],
            ["width", "100%"],
            ["height", "100%"],
            ["overflow-y","scroll"]
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

Services[WORKSPACES] = new Workspaces()

export default Services;