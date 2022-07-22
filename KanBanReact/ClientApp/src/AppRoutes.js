import { Home } from "./components/Home/Home";
import { Dashboard } from "./components/Dash/Dashboard";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        index: true,
        path: '/home',
        element: <Home />
    },
    {
        path: '/dashboard',
        element: <Dashboard />
    }
];

export default AppRoutes;
