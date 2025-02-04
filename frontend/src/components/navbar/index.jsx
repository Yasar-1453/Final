import React from 'react'
import { GrLanguage } from "react-icons/gr";
import { NavLink } from 'react-router-dom';
function Navbar() {
  return (
    <>
    <div className='flex items-center justify-between py-2 cont'>
     <NavLink to="/"><img src="/media/logo.png" alt="" /></NavLink>
      <div className='flex items-center gap-2'>
      <GrLanguage className='text-white text-xl ' />
        <NavLink to="/login" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}>LogIn</NavLink>
        <p className=' btn btn-success'>Download</p>
      </div>
    </div>
    </>
  )
}

export default Navbar
