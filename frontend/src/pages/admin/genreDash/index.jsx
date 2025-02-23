import React from 'react'
import { NavLink} from 'react-router-dom'
function GenreDashboard() {
    return (
        <div className='flex bg-white'>
            <div className='admin-left' style={{ backgroundColor: "white", width: "20%", margin: "0 auto 0 0" }}>

            </div>
            <div className='admin-right text-black' style={{ backgroundColor: "white", paddingTop: "130px", width: "80%", margin: "0 0 0 auto", height: "100vh" }}>

                <h1 className='text-center font-bold'>Genres List</h1>
                <NavLink to="/admin/genreDashboard/addGenre"><button className="admin-btn">Add Game</button></NavLink>

            </div>
        </div>
    )
}

export default GenreDashboard
