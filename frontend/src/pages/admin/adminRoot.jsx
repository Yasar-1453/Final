import React from 'react'
import { Outlet } from "react-router"
import Sidebar from '../../components/sidebar'


function AdminRoot() {
    return (
        <div>
            <Sidebar />
            <Outlet />

        </div >
    )
}

export default AdminRoot
