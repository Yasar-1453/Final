import React, { useContext, useEffect, useState } from 'react'
import { GrLanguage } from "react-icons/gr";
import { NavLink } from 'react-router-dom';
import { FaCartShopping } from "react-icons/fa6";
import { FaHeart } from "react-icons/fa";
import { favoritesContext } from '../../context/FavoritesContext';
import Swal from 'sweetalert2';
import { basketContext } from '../../context/BasketContext';
import { TiArrowSortedDown } from "react-icons/ti";
// import withReactContent from 'sweetalert2-react-content'
function Navbar() {
  let { favorites, setFavorites } = useContext(favoritesContext)
  let { basket, setBasket } = useContext(basketContext)
  let [username, setUsername] = useState(localStorage.getItem('Username'));
  let [isVisible, setIsVisible] = useState(false);

  function TrollCat() {
    Swal.fire({
      title: "Unfortunately we haven't created an app yet. With love from our team <3",
      width: 600,
      // padding: "4em",
      color: "#716add",
      background: "#fff url(/images/trees.png)",
      backdrop: `
        rgba(0,0,123,0.4)
        url("https://sweetalert2.github.io/images/nyan-cat.gif")
        left top
        no-repeat
      `,
      // showConfirmButton: true,
      // confirmButtonText: "OK",
      // customClass: {
      //   confirmButton: 'btn-custom'
      // }
    });
  }
  const logout = () => {
    localStorage.removeItem('Username');
    setUsername(null);
  };

  useEffect(() => {
    setUsername(localStorage.getItem('Username'));
  }, []);

  const toggleVisibility = () => {
    setIsVisible(prevState => !prevState);
  };

  return (
    <>
      <div className='flex items-center justify-between py-2 cont'>
        <NavLink to="/"><img src="/media/logo.png" alt="" /></NavLink>
        <div className='flex items-center gap-2'>
          <GrLanguage className='text-white text-xl ' />
          <div>
            {username ? (
              <div className='flex items-center gap-2 username'>
                <div className='' onClick={toggleVisibility}>{username}</div><span onClick={toggleVisibility}><TiArrowSortedDown /></span>
                {isVisible && <div>
                  <div className='userprofile'>
                    <NavLink to="userprofile">profile</NavLink>
                    <p className='cursor-pointer' onClick={logout}>log out</p>
                  </div>
                </div>}


              </div>

            ) : (
              <NavLink to="/login" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}>LogIn</NavLink>
            )}
          </div>
          <p className='cursor-pointer' onClick={TrollCat}>Download</p>
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
          <NavLink to="/basket" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}><FaCartShopping /></NavLink><span>{basket.length}</span>
          <NavLink to="/favorites" style={({ isActive }) => ({ color: isActive ? " #C0F001" : "white" })}><FaHeart /></NavLink><span>{favorites.length}</span>
        </div>
      </div>
    </>
  )
}

export default Navbar
