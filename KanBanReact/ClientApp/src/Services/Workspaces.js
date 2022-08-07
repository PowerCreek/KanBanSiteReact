import Services from './ServiceCollection'
import { API_URL } from './ImplementedServices'

const GetBoards = "getboards"

export default class Workspaces {

    constructor() {

    }

    Workspaces = []

    async GetWorkspacesAsync(afterFunc) {
        return await (Services["FetchFunc"](response => {
            response.json().then(json => {

                this.Workspaces = json

            })
        },
        fetch(`${Services[API_URL]}/${GetBoards}`)))
    }     
}
