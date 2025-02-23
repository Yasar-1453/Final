import React from 'react'
import { Outlet } from "react-router"
import Navbar from '../../components/navbar'
import Footer from '../../components/footer'


function UserRoot() {
    return (
        <div>
            <Navbar />
            <Outlet />
            <Footer />
        </div>
    )
}

export default UserRoot