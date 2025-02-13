import Basket from "../pages/basket"
import Details from "../pages/details"
import Favorites from "../pages/favorites"
import Games from "../pages/games"
import Home from "../pages/home"
import NotFound from "../pages/not found"
import UserRoot from "../pages/UserRoot"


const Routes = [
    {
        path: "",
        element: <UserRoot />,
        children: [
            {
                path: "/",
                element: <Home />
            },
            {
                path: "/games/:id",
                element: <Details />
            },
            {
                path: "/games",
                element:<Games/>
            },
            {
                path: "/basket",
                element: <Basket/>
            },
            {
                path: "/favorites",
                element: <Favorites />
            },
            {
                path: "*",
                element: <NotFound/>
            }
        ]
    }
]

export default Routes