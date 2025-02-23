import React from 'react'
import { NavLink } from 'react-router-dom'

function Sidebar() {
  return (
    <div className='sidebar'>
      <div className='sidebar-links'>
      <p className='sidebar-link'><NavLink to="dashboard" >DashBoard</NavLink></p>
      <p className='sidebar-link'><NavLink to="gameDashboard" >Games</NavLink></p>
      
      </div>
    </div>
  )
}

export default Sidebar
