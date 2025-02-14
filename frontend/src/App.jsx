import {RouterProvider,createBrowserRouter} from "react-router-dom"
import './App.css'
import Routes from "./routes/Routes"
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';

const routes=createBrowserRouter(Routes)
function App() {


  return (
    <>
      <RouterProvider router={routes}/>
    </>
  )
}

export default App
