import { createBrowserRouter } from 'react-router-dom';
import Menu from './Menu.jsx';

export const router = createBrowserRouter([
    {
        path: "/",
        element: <Menu />
    }
])