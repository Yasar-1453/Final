import { FaHeartBroken } from "react-icons/fa";
import React, { useContext } from 'react'
import { favoritesContext } from "../../context/FavoritesContext";




function Favorites() {
  let { favorites, setFavorites } = useContext(favoritesContext)

  function handleDeleteFavorite(id) {
    let delFav = favorites.filter(favorite => favorite.id !== id)
    setFavorites(delFav)
  }
  return (
    <div className="cont">

      {
        favorites.length == 0 ? (
          <h2 style={{ textAlign: "center" }}>Your Wish List is Empty</h2>
        ) : (
          <>
            <h1 style={{ textAlign: "center" }}>Your Wish List</h1>

            <div className="fav-cards cont">
              {
                favorites.map(favorite => (
                  <div key={favorite.id} className='fav-card'>
                    <div className="guest-img">
                      <img src={favorite.imageUrl} alt={favorite.name} />
                    </div>
                    <p style={{ fontSize: "1.2rem" }}>{favorite.name}</p>
                   <div className="flex gap-2 items-center">
                   <p style={{ cursor: "pointer" }} onClick={() => handleDeleteFavorite(favorite.id)}><FaHeartBroken /></p>
                   <p>Add to cart</p>
                   </div>
                  </div>
                ))
              }
            </div>

          </>
        )
      }

    </div>

  )
}

export default Favorites