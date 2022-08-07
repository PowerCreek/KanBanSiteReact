export default async function (onThen, fetchFunc) {
    return fetchFunc.then(response => onThen(response))
}
