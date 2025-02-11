import React, { useEffect, useState } from 'react'
import axios from "axios"
import { FaHeart } from "react-icons/fa";
let gameUrl = "http://localhost:5156/api/Game"
let genreUrl = "http://localhost:5156/api/Genre"


function Games() {
  let [data, setData] = useState([])
  let [genreData, setGenreData] = useState([])

  function getData() {
    axios.get(gameUrl)
      .then((res) => {
        setData(res.data)
      })
  }

  function getGenreData() {
    axios.get(genreUrl)
      .then((res) => {
        setGenreData(res.data)
      })
  }

  useEffect(() => {
    getData()
    getGenreData()
  }, [])
  return (
    <div>
      <div className='cont flex gap-2'>
        {
          genreData && genreData.map(genre => (
            <div key={genre.id}>
              <p>{genre.name}</p>
            </div>
          ))
        }
      </div>
      <div className='cont cards'>
        {
          data && data.map(product => (
            <div key={product.id} className='card'>

              <div className="game-img">
                <div className="overlay">
                  <div className='fav-icon'><FaHeart /></div>
                </div>
                <img src={product.imageUrl} alt="" />
              </div>
             <div className='game-info'>
             <h1>{product.name}</h1>
             <p>{product.price}$</p>
             </div>
              {/* <div style={{ display: "flex" }}>
                <NavLink to={`/products/${product._id}`} style={{ color: "black", fontSize: "20px" }}><IoIosSearch /></NavLink>
                <div style={{ cursor: "pointer" }} onClick={() => handleAddFavorite(product)}><FaHeart /></div>
              </div> */}
            </div>
          ))
        }
      </div>
    </div>
  )
}

export default Games
