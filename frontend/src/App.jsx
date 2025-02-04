import {RouterProvider,createBrowserRouter} from "react-router-dom"
import './App.css'
import Routes from "./routes/Routes"
const routes=createBrowserRouter(Routes)
function App() {


  return (
    <>
      <RouterProvider router={routes}/>
    </>
  )
}

export default App
