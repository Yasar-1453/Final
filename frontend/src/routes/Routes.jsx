import Admin from "../pages/admin"
import Basket from "../pages/basket"
import Details from "../pages/details"
import Favorites from "../pages/favorites"
import Games from "../pages/games"
import Home from "../pages/home"
import LogIn from "../pages/login"
import NotFound from "../pages/not found"
import ForgetPassword from "../pages/passwords/forgetpassword"
import ResetPassword from "../pages/passwords/resetpassword"
import Register from "../pages/register"
import UserProfile from "../pages/userprofile"
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
                path: "/admin",
                element: <Admin/>
            },
            {
                path: "/userprofile",
                element: <UserProfile/>
            },
        ]
    }
]

export default Routes