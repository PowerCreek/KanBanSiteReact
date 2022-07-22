 export default class StyleGen {

    constructor() {
    }
     
    StyleItems = []

    AddStyle(input) {
        this.StyleItems.push(input)

        return this;
    }

    GetStyles() {
        return this.StyleItems.map(a => {
            let str = []
            str.push(a.className)
            str.push("{\n\t")
            let prefix = a.style.length == 0 ? '' : "\n\t"
            if (Array.isArray(a.style))
                str.push(a.style.map(s => s.join(': ')).join(";\n\t"))
            if ((typeof a.style) == "function")
                str.push(a.style().map(s => s.join(': ')).join(";\n\t"))
            str.push(";\n}")
            return str.join('')
        })
    }
}
