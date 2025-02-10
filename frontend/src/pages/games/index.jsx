import React, { useEffect, useState } from 'react'
import axios from "axios"
function Games() {
  let [data, setData] = useState([])

  function getData() {
    axios.get("http://localhost:5156/api/Game")
      .then((res) => {
        setData(res.data)
      })
  }
  useEffect(() => {
    getData()
  }, [])
  return (
    <div className=''>
      <div className='cont cards'>
        {
          data && data.map(product => (
            <div key={product.id} className='card'>
              <img src={product.imageUrl} alt="" />
              <p>{product.name}</p>
              <p className='team-desc'>{product.description}</p>
              <p>{product.price}</p>
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
