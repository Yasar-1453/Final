import React from 'react'
import { GrLanguage } from "react-icons/gr";
import { NavLink } from 'react-router-dom';
import { FaCartShopping } from "react-icons/fa6";
import { FaHeart } from "react-icons/fa";
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
      <div className='cont flex justify-center'>
        <div className='flex gap-2'>
          <NavLink to="/" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}>Home</NavLink>
          <NavLink to="/games" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}>All Games</NavLink>
          <NavLink></NavLink>
          <NavLink></NavLink>

        </div>
        <div className='flex items-center gap-2'>
          <NavLink to="/basket" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}><FaCartShopping /></NavLink>
          <NavLink to="/favorites" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}><FaHeart /></NavLink>
        </div>
      </div>
    </>
  )
}

export default Navbar
