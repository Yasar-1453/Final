import React, { createContext, useEffect, useState } from 'react'

export let favoritesContext=createContext()

function FavoritesProvider({children}) {
    let localFavorites=JSON.parse(localStorage.getItem("favorites"))

    let [favorites,setFavorites]=useState(localFavorites ? localFavorites : [])


    useEffect(()=>{
        localStorage.setItem("favorites",JSON.stringify(favorites))
    },[favorites])

    let value={
        favorites,
        setFavorites
    }
  return (
    <favoritesContext.Provider value={value}>
      {children}
    </favoritesContext.Provider>
  )
}

export default FavoritesProvider