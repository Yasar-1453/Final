
import AddGame from "../pages/admin/addGame"
import AddGenre from "../pages/admin/addGenre"
import AdminRoot from "../pages/admin/adminRoot"
import Dashboard from "../pages/admin/dashboard"
import GameDashboard from "../pages/admin/gameDash"
import GenreDashboard from "../pages/admin/genreDash"
import Basket from "../pages/user/basket"
import Details from "../pages/user/details"
import Favorites from "../pages/user/favorites"
import Games from "../pages/user/games"
import Home from "../pages/user/home"
import LogIn from "../pages/user/login"
import NotFound from "../pages/user/not found"
import ForgetPassword from "../pages/user/passwords/forgetpassword"
import ResetPassword from "../pages/user/passwords/resetpassword"
import Register from "../pages/user/register"
import UserProfile from "../pages/user/userprofile"
import UserRoot from "../pages/user/UserRoot"



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
            },
            {
                path: "/login",
                element: <LogIn/>
            },
            {
                path: "/register",
                element: <Register/>
            },
            {
                path: "/forgetPassword",
                element: <ForgetPassword/>
            },
            {
                path: "/resetPassword",
                element: <ResetPassword/>
            },
            {
                path: "/userprofile",
                element: <UserProfile/>
            },
        ]
    },
    {
        path:"/admin",
        element:<AdminRoot/>,
        children:[
            {
                path: "dashboard",
                element: <Dashboard/>,
            },
            {
                path: "gameDashboard",
                element: <GameDashboard/>,
            },
            {
                path: "gameDashboard/addGame",
                element: <AddGame/>,
            },
            {
                path: "genreDashboard",
                element: <GenreDashboard/>,
            },
            {
                path: "genreDashboard/addGenre",
                element: <AddGenre/>,
            }
        ]
    }
]

export default Routes